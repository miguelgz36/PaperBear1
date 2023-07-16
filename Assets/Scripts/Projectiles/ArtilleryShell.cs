using Assets.Scripts.Misc;
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

    internal void DeactivedPreviewExplosion()
    {
        VolumetricDamage volumetricDamage = explosion.GetComponent<VolumetricDamage>();
        volumetricDamage.DeactivedPreviewExplosion();
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
        float angle = RotationUtils.CalculateRotationToAimObject(gameObject.transform.position, target);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
