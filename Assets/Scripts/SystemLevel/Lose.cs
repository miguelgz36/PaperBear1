using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Enemy>())
        {
            LevelStateManager.Instance.Lose();
        }
    }
}
