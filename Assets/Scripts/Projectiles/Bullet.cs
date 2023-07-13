using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 5;
    [SerializeField] protected float speed = 100;
    [SerializeField] float damage = 30;
    [SerializeField] bool isEnemy = false;
    [SerializeField] GameObject explosion;
    
    protected Rigidbody2D rigidBody;

    public float Damage { get => damage; }

    public void SetIsEnemy(bool isEnemy)
    {
        this.isEnemy = isEnemy;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    protected virtual void FixedUpdate()
    {
        rigidBody.MovePosition(speed * Time.fixedDeltaTime * transform.up + transform.position);
    }

    public void ImpactBullet()
    {
        if (explosion)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

}
