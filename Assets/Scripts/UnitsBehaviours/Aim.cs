using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected GameObject secondaryWeapon;
    [SerializeField] private GameObject objectToRotate;
    [SerializeField] private EnemyMove enemyMove;

    protected GameObject target;
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
        if (IsFromTheOpposingTeam(collision.gameObject) 
            && ShouldBeNewTarget(gameObject.gameObject) 
            && !collision.gameObject.CompareTag(objectToRotate.tag) 
            && health)
        {
            target = collision.gameObject;
            SelectWeapon(collision);
            StartCoroutine(shoot());
        }
    }

    private IEnumerator shoot()
    {
        do
        {
            yield return new WaitForSeconds(0.5f);
            primaryFireWeapon.SetIsShooting(true);
            if (secondaryWeapon) secondaryFireWeapon.SetIsShooting(true);
        }
        while (weapon.active || secondaryWeapon.active);
    }

    private bool IsFromTheOpposingTeam(GameObject gameObject)
    {
        return gameObject.tag.Contains("Enemy") && objectToRotate.tag.Contains("Allied")
                || gameObject.tag.Contains("Allied") && objectToRotate.tag.Contains("Enemy");
    }

    protected virtual void SelectWeapon(Collider2D collision)
    {
    }

    protected virtual bool ShouldBeNewTarget(GameObject newTarget)
    {
        return target == null;
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
