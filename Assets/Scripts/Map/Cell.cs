using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] int coverage = 0;
    [Range(0f, 1f)]
    [SerializeField] float speed = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (health)
        {
            health.CurrentCell = this;
        }
    }

    public bool RejectProjectile()
    {
        return Random.Range(0, 100) <= coverage;
    }
}
