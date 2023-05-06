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

    private float currentAmmo;
    private bool isShooting;
    private bool isRealoding;
    private Coroutine pullTheTriggerCourutine;
    private Coroutine startReloadingCourutine;
    private Resources resources;
    private bool isEnemy;
    public LayerMask hitMask;

    public void SetIsShooting(bool isShooting)
    {
        this.isShooting = isShooting;
        if (!isShooting)
        {
            StopFiring();
        }
    }

    private void Start()
    {
        resources = FindAnyObjectByType<Resources>();
        currentAmmo = ammoPerCharger;
        isShooting = false;
        isRealoding = false;
        isEnemy = unit.GetComponent<Unit>().IsEnemy();
    }

    private void Update()
    {
        if (currentAmmo == 0 && !isRealoding)
        {
            isRealoding = true;
            StartReloading();
        }
        else if (!isRealoding)
        {
            StartFiring();
        }
    }

    private void StartReloading()
    {
        if (startReloadingCourutine == null)
        {
            startReloadingCourutine = StartCoroutine(ReloadWeapon());
        } 
    }

    private void StartFiring()
    {
        if (isShooting)
        {
            if (pullTheTriggerCourutine == null)
            {
                pullTheTriggerCourutine = StartCoroutine(PullTheTrigger());
            }
        }
    }

    private void StopFiring()
    {
        if (pullTheTriggerCourutine != null)
        {
            StopCoroutine(pullTheTriggerCourutine);
            pullTheTriggerCourutine = null;
        }
    }

    private void StopRealoding()
    {
        if (startReloadingCourutine != null)
        {
            StopCoroutine(startReloadingCourutine);
            startReloadingCourutine = null;
            isRealoding = false;
        }
    }

    IEnumerator PullTheTrigger()
    {
        while (currentAmmo > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.up, 100, LayerMask.GetMask("Units"));
            Debug.DrawLine(firePoint.transform.position, firePoint.transform.position + firePoint.transform.up * 100);
            if (hit.collider != null)
            {
                Health enemy = hit.collider.gameObject.GetComponent<Health>();
                Debug.Log(hit.collider.gameObject);
                if (enemy != null && enemy.IsEnemy() != isEnemy)
                {
                    GameObject instancie = Instantiate(bullet, firePoint.transform.position, this.transform.rotation);
                    instancie.GetComponent<Bullet>().SetIsEnemy(isEnemy);
                    currentAmmo--;
                    if(!isEnemy) resources.ConsumeBox(ammoConsume);
                    float fireRateFinal = !isEnemy && resources.IsOutOfResources() ? fireRatePerMinute * 5 : fireRatePerMinute;
                    yield return new WaitForSeconds(fireRateFinal);
                }    
            }
            yield return new WaitForSeconds(0.1f);

        }
    }

    IEnumerator ReloadWeapon()
    {
        StopFiring();
        yield return new WaitForSeconds(reloadedRate);
        currentAmmo = ammoPerCharger;
        StopRealoding();
    }

}
