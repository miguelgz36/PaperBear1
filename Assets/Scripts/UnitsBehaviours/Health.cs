using System;
using System.Collections;
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
    private Squad squad;

    public Structure OnStructure { set => onStructure = value; }
    public Cell CurrentCell { set => currentCell = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float BaseHealth { get => baseHealth; set => baseHealth = value; }

    private void Awake()
    {
        unitController = currentUnit.GetComponent<UnitController>();
        squad = unitController.Squad;
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

    public void DoDamage(float baseDamage)
    {
        if (currentHealth <= 0) return;
        if (squad.IsMoving && !unitController.gameObject.tag.Contains("Tank")) baseDamage *= 2;
        if (onStructure != null ) baseDamage = onStructure.ReduceDamage(baseDamage);
        if (currentCell != null ) baseDamage = currentCell.ReduceDamage(baseDamage);

        if(baseDamage <= 0)
        {
            baseDamage = 1f;
        }
        currentHealth -= baseDamage;
        sliderHealth.value = currentHealth / baseHealth;
        StartCoroutine(GetComponent<HitBlink>().FlashRoutine());
        if (currentHealth <= 0 && currentUnit && currentUnit.activeSelf)
        {
            StartCoroutine(DestroyNextUpdate());
        }
    }


    private IEnumerator DestroyNextUpdate()
    {
        yield return new WaitForFixedUpdate();
        DestroyImmediate(currentUnit);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet && isEnemy != bullet.IsEnemy()) 
            {
                DoDamage(bullet.Damage);
                bullet.ImpactBullet();
            }
            VolumetricDamage volumetricDamage = collision.gameObject.GetComponent<VolumetricDamage>();
            if (volumetricDamage)
            {
                DoDamage(volumetricDamage.MaxDamage / volumetricDamage.RadiusDamage.radius);
            }
        }
    }
    
}
