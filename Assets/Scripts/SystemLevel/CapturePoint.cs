using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] Color alliedColor;
    [SerializeField] Color enemyColor;
    [SerializeField] Color neutralColor;


    private CapturePointStateEnum state = CapturePointStateEnum.NEUTRAL;
    private SpriteRenderer spriteRenderer;

    public CapturePointStateEnum State { get => state; }

    private void Awake()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
       spriteRenderer.color = neutralColor;
    }

    public void SetState(CapturePointStateEnum state)
    {
        this.state = state;
        spriteRenderer.color = state switch
        {
            CapturePointStateEnum.ALLIED => alliedColor,
            CapturePointStateEnum.ENEMY => enemyColor,
            _ => neutralColor,
        };
    }
}
