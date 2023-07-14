using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumetricDamage : MonoBehaviour
{
    [SerializeField] GameObject explosionFx;
    [SerializeField] GameObject explosionPreview;

    [SerializeField] float maxRadius;
    [SerializeField] float speedExplosion;
    [SerializeField] float maxDamage = 100f;

    private GameObject instantiatePreview;
    CircleCollider2D radiusDamage;

    public CircleCollider2D RadiusDamage { get => radiusDamage;}
    public float MaxDamage { get => maxDamage;}

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

    internal void DeactivedPreviewExplosion()
    {
        instantiatePreview.GetComponent<ExplosionPreview>().Deactived();
    }

    internal void ActivedPreviewExplosion()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;
        instantiatePreview = Instantiate(explosionPreview, mousePosition, Quaternion.identity);
        instantiatePreview.GetComponent<ExplosionPreview>().Actived();
    }
}
