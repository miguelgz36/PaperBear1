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
        else
        {
            SelectPrimaryWeapon();
        }
    }

    protected override bool ShouldBeNewTarget(GameObject newTarget)
    {
        return base.ShouldBeNewTarget(newTarget) || (!IsTank(base.target) && IsTank(newTarget));
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
}
