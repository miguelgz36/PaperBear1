using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trench : MonoBehaviour
{

    [SerializeField] int probabilityToRejectProjectile = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            int random = Random.Range(0, 100);
            Debug.Log("Random" + random);
            if (random <= probabilityToRejectProjectile)
            {
                Debug.Log("PROJECTILE REJECTED");
                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);
            }
        }
    }
}
