using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryShell : Bullet
{
    [SerializeField] float proximityThreshold = 0.1f;
    Transform target;

    protected override void Start()
    {
        FireShell(FindAnyObjectByType<Enemy>().gameObject.transform);
    }
    protected override void FixedUpdate()
    {  
        if (target)
        {
            if (Vector3.Distance(transform.position, target.position) < proximityThreshold)
            {
                ImpactBullet();
            }
            rigidBody.MovePosition(speed * Time.fixedDeltaTime * transform.up + transform.position);
        }
    }

    public void FireShell(Transform target)
    {
        this.target = target;
        DefineRotation();
    }

    private void DefineRotation()
    {
        float angle = Mathf.Atan2(target.transform.position.y - gameObject.transform.position.y,
                          target.transform.position.x - gameObject.transform.position.x)
              * Mathf.Rad2Deg - 90;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
