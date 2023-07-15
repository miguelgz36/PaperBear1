using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery : Placeable
{
    [SerializeField] GameObject projectile;
    [SerializeField] int amount = 1;
    [SerializeField] float delaySplashs = 1f;
    bool firing = true;

    public void ActivedPreviewExplosion()
    {
        ArtilleryShell artilleryShell = projectile.GetComponent<ArtilleryShell>();
        if (artilleryShell)
        {
            artilleryShell.ActivedPreviewExplosion();
        }
    }

    private void Update()
    {
        if (!firing) Destroy(this.gameObject);
    }

    public void FireShells(Vector3 target)
    {
        StartCoroutine(StartFiring(target));
        DeactivedPreviewExplosion();
    }

    public IEnumerator StartFiring(Vector3 target)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(delaySplashs);
            GameObject projectileInsantiate = Instantiate(projectile, SupportFireManager.Instance.PositionAlliedSupportingFire.position, Quaternion.identity);
            projectileInsantiate.GetComponent<ArtilleryShell>().FireShell(target);
        }
        firing = false;
    }

    public void DeactivedPreviewExplosion()
    {
        ArtilleryShell artilleryShell = projectile.GetComponent<ArtilleryShell>();
        if (artilleryShell)
        {
            artilleryShell.DeactivedPreviewExplosion();
        }
    }
}
