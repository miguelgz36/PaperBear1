using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float speed = 5;

    private bool move;

    public void SetIsMove(bool move)
    {
        this.move = move;
    }

    private void Start()
    {
        move = true;
    }

    void Update()
    {
        if (move)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position += speed * Time.deltaTime * transform.up;
    }
}
