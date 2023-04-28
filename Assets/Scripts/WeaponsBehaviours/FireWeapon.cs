using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject firePoint;
    [SerializeField] float fireRatePerMinute = 1;
    [SerializeField] float precision = 1;
    [SerializeField] float ammoPerCharger = 12;
    [SerializeField] float reloadedRate = 10;

    private float currentAmmo;
    private bool isShooting;
    private bool isRealoding;
    private Coroutine pullTheTriggerCourutine;
    private Coroutine startReloadingCourutine;


    public void SetIsShooting(bool isShooting)
    {
        this.isShooting = isShooting;
    }

    private void Start()
    {
        currentAmmo = ammoPerCharger;
        isShooting = true;
        isRealoding = false;
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
            GameObject instanceBullet = Instantiate(bullet, firePoint.transform.position, this.transform.rotation);
            currentAmmo--;
            yield return new WaitForSeconds(fireRatePerMinute);
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