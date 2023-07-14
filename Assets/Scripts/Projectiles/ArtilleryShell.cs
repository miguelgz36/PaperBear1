using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryShell : Bullet
{
    [SerializeField] float proximityThreshold = 0.1f;
    [SerializeField] float delayFire = 3f;
    [SerializeField] Sound soundFire;
    [SerializeField] Sound soundIncoming;

    Vector3 target;
    bool fire = false;

    
    protected override void FixedUpdate()
    {
        if (fire)
        {
            if (Vector3.Distance(transform.position, target) < proximityThreshold)
            {
                ImpactBullet();
            }
            rigidBody.MovePosition(speed * Time.fixedDeltaTime * transform.up + transform.position);
        }
    }

    internal void ActivedPreviewExplosion()
    {
        VolumetricDamage volumetricDamage = explosion.GetComponent<VolumetricDamage>();
        if (volumetricDamage)
        {
            volumetricDamage.ActivedPreviewExplosion();
        }
    }

    public void FireShell(Vector3 target)
    {
        this.target = target;
        DefineRotation();
        VolumetricDamage volumetricDamage = explosion.GetComponent<VolumetricDamage>();
        volumetricDamage.DeactivedPreviewExplosion();
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        soundFire.PlayAtPoint();
        yield return new WaitForSeconds(delayFire);
        fire = true;
        soundIncoming.Play();
    }

    private void DefineRotation()
    {
        float angle = Mathf.Atan2(target.y - gameObject.transform.position.y,
                          target.x - gameObject.transform.position.x)
              * Mathf.Rad2Deg - 90;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
