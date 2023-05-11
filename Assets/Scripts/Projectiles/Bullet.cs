using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 5;
    [SerializeField] float speed = 100;
    [SerializeField] bool isEnemy = false;
    
    bool impactedEnemy = false;

    private new Rigidbody2D rigidbody;

    public bool ImpactedEnemy { get => impactedEnemy; set => impactedEnemy = value; }

    public void SetIsEnemy(bool isEnemy)
    {
        this.isEnemy = isEnemy;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyBullet());
    }
    private void FixedUpdate()
    {
        if (impactedEnemy) return;
        rigidbody.MovePosition(speed * Time.fixedDeltaTime * transform.up + transform.position);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

}
