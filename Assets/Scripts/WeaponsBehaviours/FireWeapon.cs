using System.Collections;
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
    [SerializeField] float dispersion = .1f;
    [SerializeField] float percentajeVarianceFireRate = 0.2f;
    [SerializeField] float rangeFire = 10f;
    [SerializeField] int burstAmount = 5;
    [SerializeField] float fireRateBurst = 1f;
    [SerializeField] bool primaryWeapon = true;
    [SerializeField] bool burst = false;

    private float currentAmmo;
    private bool startShooting;
    private bool isShooting;
    private bool isRealoding;
    private Sound soundWeapon;
    private float varianceFireRate;
    private float initialReloadedTime;
    private Health target;

    private void Awake()
    {
        soundWeapon = GetComponent<Sound>();
    }
    public void SetIsShooting(bool startShooting, Health target)
    {
        this.startShooting = startShooting;
        this.target = target;
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
            isRealoding = true;
            initialReloadedTime = Time.unscaledTime;
        }
        else if (isRealoding)
        {
            float currentTime = Time.unscaledTime - initialReloadedTime;
            if (currentTime < reloadedRate)
            {
                if (primaryWeapon) unitController.SliderAmmo.value = currentTime / reloadedRate;
            }
            else
            {
                if (primaryWeapon) unitController.SliderAmmo.value = 1;
                currentAmmo = ammoPerCharger;
                isRealoding = false;
            }
        }
        else if (startShooting && !isRealoding && !isShooting)
        {
            StartCoroutine(PullTheTrigger());
        }
    }



    IEnumerator PullTheTrigger()
    {
        while (currentAmmo > 0 && startShooting)
        {
            isShooting = true;
            if (target != null && target.IsEnemy() != unitController.IsEnemy() && IsInRangeFire(target))
            {
                if (burst)
                {
                    int burstCount = 0;
                    soundWeapon.PlayAtPoint();
                    while (burstCount < burstAmount)
                    {
                        float finalFireRate = Shot();
                        yield return new WaitForSeconds(finalFireRate);
                        burstCount++;
                    }
                }
                else
                {
                    float finalFireRate = Shot();
                    yield return new WaitForSeconds(finalFireRate);
                }

            }
            else
            {
                yield return new WaitForSeconds(1f);
            }

            if (burst)
            {
                yield return new WaitForSeconds(fireRateBurst);
            }
        }
        isShooting = false;
    }

    private float Shot()
    {
        Quaternion rotation = this.transform.rotation;
        rotation.z += Random.Range(-dispersion, dispersion);
        GameObject instancie = Instantiate(bullet, firePoint.transform.position, rotation);
        instancie.GetComponent<Bullet>().SetIsEnemy(unitController.IsEnemy());
        if (!burst) soundWeapon.PlayAtPoint();
        currentAmmo--;
        if (primaryWeapon) unitController.SliderAmmo.value = currentAmmo / ammoPerCharger;
        float finalFireRate = Random.Range(fireRatePerMinute - varianceFireRate, fireRatePerMinute + varianceFireRate);
        return finalFireRate;
    }

    public bool IsInRangeFire(Health enemy)
    {
        return Vector2.Distance(transform.position, enemy.transform.position) < rangeFire;
    }

}
