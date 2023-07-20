using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron : MonoBehaviour
{
    [SerializeField] float speed;
    bool deployed = false;
    Vector3 target;
    protected Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (deployed)
        {
            if (Vector3.Distance(transform.position, target) < 0.3f)
            {
                deployed = false;
            }
            else
            {
                rigidBody.MovePosition(speed * Time.fixedDeltaTime * transform.up + transform.position);
            }
        }
    }

    public void Deploy(Vector3 target)
    {
        this.target = target;
        DefineRotation();
        this.deployed = true;
    }

    private void DefineRotation()
    {
        float angle = Mathf.Atan2(target.y - gameObject.transform.position.y,
                          target.x - gameObject.transform.position.x)
              * Mathf.Rad2Deg - 90;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
