using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Artillery : Placeable
{
    [SerializeField] GameObject projectile;
    [SerializeField] int amount = 1;
    [SerializeField] float delaySplashs = 1f;
    [SerializeField] private float maxDispersion = 0f;
    [SerializeField] private Transform particles;

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
        if (!firing) Destroy(this.gameObject, 10f);
    }

    public virtual void FireShells(Vector3 target, Vector3 origin)
    {
        particles.position = target;
        StartCoroutine(StartFiring(target, maxDispersion, origin));
        DeactivedPreviewExplosion();
    }

    public IEnumerator StartFiring(Vector3 target, float offset, Vector3 origin)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(delaySplashs);
            float x = Random.Range(target.x - offset, target.x + offset);
            float y = Random.Range(target.y - offset, target.y + offset);
            GameObject projectileInsantiate = Instantiate(projectile, origin, Quaternion.identity);
            projectileInsantiate.GetComponent<ArtilleryShell>().FireShell(new Vector3(x, y));
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
