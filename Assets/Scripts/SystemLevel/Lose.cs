using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{

    private LevelStateManager levelStateManager;

    private void Awake()
    {
        levelStateManager = FindAnyObjectByType<LevelStateManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Enemy>())
        {
            levelStateManager.Lose();
        }
    }
}
