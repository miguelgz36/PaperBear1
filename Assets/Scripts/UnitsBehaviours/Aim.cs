using Assets.Scripts.Misc;
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

            float angle = RotationUtils.CalculateRotationToAimObject(gameObject.transform.position, target.transform.position);

            objectToRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    public void AimTarget(Collider2D collision)
    {
        LockTarget(collision);
    }

    protected void LockTarget(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (collision.gameObject && IsFromTheOpposingTeam(collision.gameObject) 
            && ShouldBeNewTarget(gameObject.gameObject) 
            && !collision.gameObject.CompareTag(objectToRotate.tag) 
            && health)
        {
            target = collision.gameObject;
            SelectWeapon(collision);
            StartCoroutine(Shoot(health));
        }
    }

    private IEnumerator Shoot(Health target)
    {
        do
        {
            yield return new WaitForSeconds(0.5f);
            if(target != null)
            {
                primaryFireWeapon.SetIsShooting(true, target);
                if (secondaryWeapon) secondaryFireWeapon.SetIsShooting(true, target);
            }         
        }
        while (!weapon.active || (secondaryWeapon != null && !secondaryWeapon.active));
    }

    private bool IsFromTheOpposingTeam(GameObject gameObject)
    {
        if (objectToRotate == null || gameObject == null) return false;
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

    public void UnAimTarget(Collider2D collision)
    {
        if(target != null && target == collision.gameObject)
        {
            target = null;
            objectToRotate.transform.rotation = startedRotation;
            primaryFireWeapon.SetIsShooting(false, null);
            if (secondaryWeapon) secondaryFireWeapon.SetIsShooting(false, null);
            if (enemyMove != null) enemyMove.SetIsMove(true);
        }
    }

}
