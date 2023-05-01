using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private GameObject currentUnit;
    [SerializeField] private float baseHealth = 100;

    private float currentHealth;
    
    public void GetDamage(float baseDamage)
    {
        currentHealth -= baseDamage;
        if(currentHealth <= 0)
        {
            Destroy(currentUnit);
        }
    }
    void Start()
    {
        currentHealth = baseHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            collision.gameObject.SetActive(false);
            GetDamage(20);
            Destroy(collision.gameObject);
        }
    }
    
}
