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

    public Structure OnStructure { get => onStructure; set => onStructure = value; }

    public bool IsEnemy()
    {
        return isEnemy;
    }
    
    public void GetDamage(float baseDamage)
    {
        if (onStructure != null && onStructure.RejectProjectile()) return;
        currentHealth -= baseDamage;
        if(currentHealth <= 0)
        {
            Destroy(currentUnit);
        }
    }
    void Start()
    {
        currentHealth = baseHealth;
        isEnemy = currentUnit.GetComponent<Unit>().IsEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (isEnemy != bullet.IsEnemy()) 
            {
                GetDamage(20);
            }
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
    
}
