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
        StartCoroutine(DestroyBullet());
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(speed * Time.fixedDeltaTime * transform.up + transform.position);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

}
