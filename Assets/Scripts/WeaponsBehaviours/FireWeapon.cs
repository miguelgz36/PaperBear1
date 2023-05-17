using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject firePoint;
    [SerializeField] UnitController unitController;
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
    private SoundWeapon soundWeapon;

    private void Awake()
    {
        soundWeapon = GetComponent<SoundWeapon>();
        resources = FindAnyObjectByType<Resources>();
    }
    public void SetIsShooting(bool startShooting)
    {
        this.startShooting = startShooting;
    }

    private void Start()
    {
        currentAmmo = ammoPerCharger;
        startShooting = false;
        isRealoding = false;
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
        while (currentAmmo > 0 && (unitController.IsEnemy() || !resources.IsOutOfResources()) && startShooting)
        {
            isShooting = true;
            RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.up, 100, LayerMask.GetMask("Units"));
            if (hit.collider != null)
            {
                Health enemy = hit.collider.gameObject.GetComponent<Health>();
                if (enemy != null && enemy.IsEnemy() != unitController.IsEnemy())
                {
                    Quaternion rotation = this.transform.rotation;
                    rotation.z += Random.Range(-dispersion,dispersion);
                    GameObject instancie = Instantiate(bullet, firePoint.transform.position, rotation);
                    instancie.GetComponent<Bullet>().SetIsEnemy(unitController.IsEnemy());
                    soundWeapon.PlaySoundFire();
                    currentAmmo--;
                    if (!unitController.IsEnemy()) resources.ConsumeBox(ammoConsume);
                    yield return new WaitForSeconds(fireRatePerMinute);
                }              
            }
            yield return new WaitForSeconds(0.01f);

        }
        isShooting = false;
    }

    IEnumerator ReloadWeapon()
    {
        unitController.StartReloading();
        isRealoding = true;
        yield return new WaitForSeconds(reloadedRate);
        currentAmmo = ammoPerCharger;
        isRealoding = false;
        unitController.StopReloading();
    }

}
