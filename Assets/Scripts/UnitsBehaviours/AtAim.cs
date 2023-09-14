using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtAim: Aim
{


    protected override bool ShouldBeNewTarget(GameObject newTarget)
    {
        return base.ShouldBeNewTarget(newTarget) || (!IsTank(base.target) && IsTank(newTarget));
    }


    private bool IsTank(GameObject gameObject)
    {
        return gameObject.tag.Contains("Tank");
    }
}
