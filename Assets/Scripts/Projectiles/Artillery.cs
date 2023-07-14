using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery : Placeable
{
    [SerializeField] GameObject projectile;
    [SerializeField] int amount;

    internal void ActivedPreviewExplosion()
    {
        ArtilleryShell artilleryShell = projectile.GetComponent<ArtilleryShell>();
        if (artilleryShell)
        {
            artilleryShell.ActivedPreviewExplosion();
        }
    }

    internal void FireShell(Vector3 target)
    {
        GameObject projectileInsantiate = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileInsantiate.GetComponent<ArtilleryShell>().FireShell(target);
    }
}
