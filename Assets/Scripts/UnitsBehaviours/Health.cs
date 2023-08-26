using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] private GameObject currentUnit;
    [SerializeField] private float baseHealth = 100;
    [SerializeField] private Slider sliderHealth;

    private float currentHealth;
    private bool isEnemy;
    private Structure onStructure;
    private Cell currentCell;
    private UnitController unitController;

    public Structure OnStructure { set => onStructure = value; }
    public Cell CurrentCell { set => currentCell = value; }

    private void Awake()
    {
        unitController = currentUnit.GetComponent<UnitController>();
    }

    void Start()
    {
        currentHealth = baseHealth;
        isEnemy = unitController.IsEnemy();
    }


    public bool IsEnemy()
    {
        return isEnemy;
    }

    public bool DoDamage(float baseDamage)
    {
        if (onStructure != null ) baseDamage = onStructure.ReduceDamage(baseDamage);
        if (currentCell != null ) baseDamage = currentCell.ReduceDamage(baseDamage);

        if(baseDamage <= 0)
        {
            baseDamage = 1f;
        }
        currentHealth -= baseDamage;
        sliderHealth.value = currentHealth / baseHealth;
        StartCoroutine(GetComponent<HitBlink>().FlashRoutine());
        if (currentHealth <= 0)
        {
            unitController.gameObject.SetActive(false);
            Destroy(currentUnit);
        }
        return true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet && isEnemy != bullet.IsEnemy()) 
            {
                bool didDamage = DoDamage(bullet.Damage);
                if (didDamage) bullet.ImpactBullet();
            }
            VolumetricDamage volumetricDamage = collision.gameObject.GetComponent<VolumetricDamage>();
            if (volumetricDamage)
            {
                DoDamage(volumetricDamage.MaxDamage / volumetricDamage.RadiusDamage.radius);
            }
        }
    }
    
}
