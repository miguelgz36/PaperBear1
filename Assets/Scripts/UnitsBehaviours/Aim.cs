using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject currentUnit;

    private GameObject target;
    private Quaternion startedRotation;
    private FireWeapon fireWeapon;
    private EnemyMove enemyMove;

    private void Start()
    {
        startedRotation = currentUnit.transform.rotation;
        fireWeapon = weapon.GetComponent<FireWeapon>();
        enemyMove = currentUnit.GetComponent<EnemyMove>();
    }
    private void Update()
    {
        AimTarger();
    }

    private void AimTarger()
    {
        if (target != null && target.activeSelf)
        {
            float angle = Mathf.Atan2(target.transform.position.y - currentUnit.transform.position.y,
                          target.transform.position.x - currentUnit.transform.position.x)
              * Mathf.Rad2Deg - 90;
            currentUnit.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (target == null && !collision.gameObject.CompareTag(currentUnit.tag) && health)
        {
            Debug.Log(collision.gameObject.tag + " " + currentUnit.tag);
            target = collision.gameObject;
            AimTarger();
            fireWeapon.SetIsShooting(true);
            if (enemyMove != null) enemyMove.SetIsMove(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(target != null && target == collision.gameObject)
        {
            target = null;
            currentUnit.transform.rotation = startedRotation;
            fireWeapon.SetIsShooting(false);
            if (enemyMove != null) enemyMove.SetIsMove(true);
        }
    }

}
