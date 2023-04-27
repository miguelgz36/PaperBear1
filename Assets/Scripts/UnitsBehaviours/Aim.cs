using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private GameObject currentUnit;

    private void Update()
    {
        AimTarger();
    }

    private void AimTarger()
    {
        if (target != null)
        {
            float angle = Mathf.Atan2(target.transform.position.y - currentUnit.transform.position.y,
                          target.transform.position.x - currentUnit.transform.position.x)
              * Mathf.Rad2Deg - 90;
            currentUnit.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == null && !collision.gameObject.CompareTag(currentUnit.tag))
        {
            Debug.Log("is aiming");
            target = collision.gameObject;
        }
    }

}
