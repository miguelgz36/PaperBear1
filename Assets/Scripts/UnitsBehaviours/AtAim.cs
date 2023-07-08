using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtAim: Aim
{

    protected override void SelectWeapon(Collider2D collision)
    {
        if (IsTank(collision.gameObject))
        {
            SelectRpg();
        } 
        if(IsEnemyNoTank(collision.gameObject))
        {
            SelectPrimaryWeapon();
        }
    }

    protected override bool ShouldBeNewTarget(GameObject newTarget)
    {
        return base.ShouldBeNewTarget(newTarget) || IsTank(newTarget);
    }

    private void SelectRpg()
    {
        if (base.weapon.active || !base.secondaryWeapon.active)
        {
            base.weapon.SetActive(false);
            base.secondaryWeapon.SetActive(true);
        }

    }

    private void SelectPrimaryWeapon()
    {
        if (!base.weapon.active || base.secondaryWeapon.active)
        {
            base.weapon.SetActive(true);
            base.secondaryWeapon.SetActive(false);
        }
    }

    private bool IsTank(GameObject gameObject)
    {
        return gameObject.tag.Contains("Tank");
    }

    private bool IsEnemyNoTank(GameObject gameObject)
    {
        return gameObject.tag.Equals("Enemy");
    }
}
