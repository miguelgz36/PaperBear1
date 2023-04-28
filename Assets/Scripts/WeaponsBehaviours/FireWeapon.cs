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
    private Coroutine pullTheTriggerCourutine;

    public void SetIsShooting(bool isShooting)
    {
        this.isShooting = isShooting;
    }

    private void Start()
    {
        currentAmmo = ammoPerCharger;
        isShooting = true;
    }

    private void Update()
    {
        if (isShooting)
        {
            if (pullTheTriggerCourutine == null)
            {
                pullTheTriggerCourutine = StartCoroutine(PullTheTrigger());
            }
        }
        else
        {
            if (pullTheTriggerCourutine != null)
            {
                StopCoroutine(pullTheTriggerCourutine);
                pullTheTriggerCourutine = null;
            }
        }
    }

    IEnumerator PullTheTrigger()
    {
        while (true)
        {
            GameObject instanceBullet = Instantiate(bullet, firePoint.transform.position, this.transform.rotation);
            currentAmmo--;
            yield return new WaitForSeconds(fireRatePerMinute);
        }
    }

}
