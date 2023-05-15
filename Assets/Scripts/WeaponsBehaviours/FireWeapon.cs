using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject unit;
    [SerializeField] float fireRatePerMinute = 1;
    [SerializeField] float ammoPerCharger = 12;
    [SerializeField] float reloadedRate = 10;
    [SerializeField] float ammoConsume = 1;
    [SerializeField] float dispersion = .1f;

    private float currentAmmo;
    private bool startShooting;
    private bool isShooting;
    private bool isRealoding;
    private Resources resources;
    private bool isEnemy;
    public LayerMask hitMask;

    public void SetIsShooting(bool startShooting)
    {
        this.startShooting = startShooting;
    }

    private void Start()
    {
        resources = FindAnyObjectByType<Resources>();
        currentAmmo = ammoPerCharger;
        startShooting = false;
        isRealoding = false;
        isEnemy = unit.GetComponent<Unit>().IsEnemy();
    }

    private void Update()
    {
        if (currentAmmo == 0 && !isRealoding)
        {
           StartCoroutine(ReloadWeapon());
        }
        else if (startShooting && !isRealoding && !isShooting)
        {
           StartCoroutine(PullTheTrigger());
        }
    }

 

    IEnumerator PullTheTrigger()
    {
        while (currentAmmo > 0 && (isEnemy || !resources.IsOutOfResources()))
        {
            isShooting = true;
            RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.up, 100, LayerMask.GetMask("Units"));
            if (hit.collider != null)
            {
                Health enemy = hit.collider.gameObject.GetComponent<Health>();
                if (enemy != null && enemy.IsEnemy() != isEnemy)
                {
                    Quaternion rotation = this.transform.rotation;
                    rotation.z += Random.Range(-dispersion,dispersion);
                    GameObject instancie = Instantiate(bullet, firePoint.transform.position, rotation);
                    instancie.GetComponent<Bullet>().SetIsEnemy(isEnemy);
                    currentAmmo--;
                    if (!isEnemy) resources.ConsumeBox(ammoConsume);
                    yield return new WaitForSeconds(fireRatePerMinute);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        isShooting = false;
    }

    IEnumerator ReloadWeapon()
    {
        isRealoding = true;
        yield return new WaitForSeconds(reloadedRate);
        currentAmmo = ammoPerCharger;
        isRealoding = false;
    }

}
