using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPreview : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;

        transform.position = mousePosition;
    }

    internal void Actived()
    {
        spriteRenderer.enabled = true;
    }

}
