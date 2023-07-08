using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumetricDamage : MonoBehaviour
{
    [SerializeField] GameObject explosionFx;

    [SerializeField] float maxRadius;
    [SerializeField] float speedExplosion;


    CircleCollider2D radiusDamage;

    public CircleCollider2D RadiusDamage { get => radiusDamage;}

    private void Awake()
    {
        radiusDamage = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        Instantiate(explosionFx, this.transform.position, this.transform.rotation);
    }

    private void Update()
    {
        Debug.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + (Vector3.up * RadiusDamage.radius), Color.black, Time.deltaTime);
        Debug.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + (Vector3.left * RadiusDamage.radius), Color.black, Time.deltaTime);
        Debug.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + (Vector3.right * RadiusDamage.radius), Color.black, Time.deltaTime);
        Debug.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + (Vector3.down * RadiusDamage.radius), Color.black, Time.deltaTime);
        if (RadiusDamage.radius < maxRadius)
        {
            float nextRadius = RadiusDamage.radius + (speedExplosion * Time.deltaTime);
            bool isInMaxRadius = nextRadius > maxRadius;
            if (isInMaxRadius)
            {
                nextRadius = maxRadius;
            }
            RadiusDamage.radius = nextRadius;
            if (isInMaxRadius)
            {
                Destroy(gameObject);
            }
        }


    }
}
