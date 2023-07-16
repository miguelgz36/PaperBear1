using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject aimPointVehicle;
    [SerializeField] UnitController unitController;
    [SerializeField] float fireRatePerMinute = 1;
    [SerializeField] float ammoPerCharger = 12;
    [SerializeField] float reloadedRate = 10;
    [SerializeField] float ammoConsume = 1;
    [SerializeField] float dispersion = .1f;
    [SerializeField] float percentajeVarianceFireRate = 0.2f;
    [SerializeField] float rangeFire = 10f;

    private float currentAmmo;
    private bool startShooting;
    private bool isShooting;
    private bool isRealoding;
    private Sound soundWeapon;
    private float varianceFireRate;

    private void Awake()
    {
        soundWeapon = GetComponent<Sound>();
    }
    public void SetIsShooting(bool startShooting)
    {
        this.startShooting = startShooting;
    }

    private void Start()
    {
        varianceFireRate = percentajeVarianceFireRate * fireRatePerMinute;
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
        while (currentAmmo > 0 && (unitController.IsEnemy() || !Resources.Instance.IsOutOfResources()) && startShooting)
        {
            isShooting = true;
            RaycastHit2D hit = RayCastToTarget();
            if (hit.collider != null)
            {
                Health enemy = hit.collider.gameObject.GetComponent<Health>();
                if (enemy != null && enemy.IsEnemy() != unitController.IsEnemy() && IsInRangeFire(enemy))
                {
                    float finalFireRate = Shot();
                    yield return new WaitForSeconds(finalFireRate);
                }
            }
            yield return new WaitForSeconds(0.01f);

        }
        isShooting = false;
    }

    private float Shot()
    {
        Quaternion rotation = this.transform.rotation;
        rotation.z += Random.Range(-dispersion, dispersion);
        GameObject instancie = Instantiate(bullet, firePoint.transform.position, rotation);
        instancie.GetComponent<Bullet>().SetIsEnemy(unitController.IsEnemy());
        soundWeapon.PlayAtPoint();
        currentAmmo--;
        if (!unitController.IsEnemy()) Resources.Instance.ConsumeBox(ammoConsume);
        float finalFireRate = Random.Range(fireRatePerMinute - varianceFireRate, fireRatePerMinute + varianceFireRate);
        return finalFireRate;
    }

    public bool IsInRangeFire(Health enemy)
    {
        return Vector2.Distance(transform.position, enemy.transform.position) < rangeFire;
    }

    private RaycastHit2D RayCastToTarget()
    {
        return Physics2D.Raycast(aimPointVehicle ? aimPointVehicle.transform.position : firePoint.transform.position,
            aimPointVehicle ? aimPointVehicle.transform.up : firePoint.transform.up,
            100, LayerMask.GetMask("Units"));
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
