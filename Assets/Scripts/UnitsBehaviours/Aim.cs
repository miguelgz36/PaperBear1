using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject secondaryWeapon;
    [SerializeField] private GameObject objectToRotate;
    [SerializeField] private EnemyMove enemyMove;

    private GameObject target;
    private Quaternion startedRotation;
    private FireWeapon primaryFireWeapon;
    private FireWeapon secondaryFireWeapon;

    private void Awake()
    {
        startedRotation = objectToRotate.transform.rotation;
        primaryFireWeapon = weapon.GetComponent<FireWeapon>();
        if (secondaryWeapon) secondaryFireWeapon = secondaryWeapon.GetComponent<FireWeapon>();
    }
    private void Update()
    {
        AimTarger();
    }

    private void AimTarger()
    {
        if (target != null && target.activeSelf)
        {
            if (enemyMove != null && primaryFireWeapon.IsInRangeFire(target.GetComponent<Health>())) enemyMove.SetIsMove(false);

            float angle = Mathf.Atan2(target.transform.position.y - objectToRotate.transform.position.y,
                          target.transform.position.x - objectToRotate.transform.position.x)
              * Mathf.Rad2Deg - 90;
            objectToRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        LockTarget(collision);
    }

    protected void LockTarget(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (target == null
             && health
             && (collision.gameObject.tag.Contains("Enemy") && objectToRotate.tag.Contains("Allied")
                || collision.gameObject.tag.Contains("Allied") && objectToRotate.tag.Contains("Enemy")))
        {
            if (target == null && !collision.gameObject.CompareTag(objectToRotate.tag) && health)
            {
                target = collision.gameObject;
                primaryFireWeapon.SetIsShooting(true);
                if (secondaryWeapon) secondaryFireWeapon.SetIsShooting(true);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(target != null && target == collision.gameObject)
        {
            target = null;
            objectToRotate.transform.rotation = startedRotation;
            primaryFireWeapon.SetIsShooting(false);
            if (secondaryWeapon) secondaryFireWeapon.SetIsShooting(false);
            if (enemyMove != null) enemyMove.SetIsMove(true);
        }
    }

}
