using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private GameObject currentUnit;
    [SerializeField] private float baseHealth = 100;

    private float currentHealth;
    private bool isEnemy;
    private Structure onStructure;
    private Cell currentCell;

    public Structure OnStructure { set => onStructure = value; }
    public Cell CurrentCell { set => currentCell = value; }



    void Start()
    {
        currentHealth = baseHealth;
        isEnemy = currentUnit.GetComponent<UnitController>().IsEnemy();
    }


    public bool IsEnemy()
    {
        return isEnemy;
    }

    public bool DoDamage(float baseDamage)
    {
        if (onStructure != null && onStructure.RejectProjectile()) return false;
        if (currentCell != null && currentCell.RejectProjectile()) return false;

        currentHealth -= baseDamage;
        StartCoroutine(GetComponent<HitBlink>().FlashRoutine());
        if (currentHealth <= 0)
        {
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
