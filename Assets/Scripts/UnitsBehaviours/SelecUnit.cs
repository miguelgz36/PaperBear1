using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecUnit : MonoBehaviour
{

    [SerializeField] private float speedChangeAlpha = 0.1f;
    private SpriteRenderer sprite;
    private readonly float initAlpha = 1.0f;
    private float minAlpha = 0.2f;
    private int direction = -1;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float nextAlpha = sprite.color.a + (speedChangeAlpha * Time.deltaTime * direction);
        if (nextAlpha < minAlpha)
        {
            direction = 1;
            nextAlpha = minAlpha;
        } else if (nextAlpha > initAlpha)
        {
            direction = -1;
            nextAlpha = initAlpha;
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, nextAlpha);
    }
}
