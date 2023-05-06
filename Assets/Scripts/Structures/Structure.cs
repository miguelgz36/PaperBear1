using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{

    [SerializeField] int probabilityToRejectProjectile = 50;

    public bool RejectProjectile()
    {
        return Random.Range(0, 100) <= probabilityToRejectProjectile;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.OnStructure = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.OnStructure = null;
        }
    }
}
