using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject objectToRotate;
    [SerializeField] private EnemyMove enemyMove;
    [SerializeField] private bool primaryAim = true;


    private GameObject target;
    private Quaternion startedRotation;
    private FireWeapon fireWeapon;

    private void Awake()
    {
        startedRotation = objectToRotate.transform.rotation;
        fireWeapon = weapon.GetComponent<FireWeapon>();
    }
    private void Update()
    {
        AimTarger();
    }

    private void AimTarger()
    {
        if (target != null && target.activeSelf)
        {
            float angle = Mathf.Atan2(target.transform.position.y - objectToRotate.transform.position.y,
                          target.transform.position.x - objectToRotate.transform.position.x)
              * Mathf.Rad2Deg - 90;
            objectToRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if(!collision.gameObject.CompareTag(objectToRotate.tag) && health)
        {
            if (enemyMove != null && primaryAim && fireWeapon.IsInRangeFire(health)) enemyMove.SetIsMove(false);
            if (target == null && !collision.gameObject.CompareTag(objectToRotate.tag) && health)
            {
                target = collision.gameObject;
                fireWeapon.SetIsShooting(true);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(target != null && target == collision.gameObject)
        {
            target = null;
            objectToRotate.transform.rotation = startedRotation;
            fireWeapon.SetIsShooting(false);
            if (enemyMove != null) enemyMove.SetIsMove(true);
        }
    }

}
