using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 5;
    [SerializeField] float speed = 100;

    private new Rigidbody2D rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = speed * Time.deltaTime * transform.up;

        StartCoroutine(DestroyBullet());
    }
    private void FixedUpdate()
    {
        rigidbody.velocity = speed * Time.fixedDeltaTime * transform.up;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

}
