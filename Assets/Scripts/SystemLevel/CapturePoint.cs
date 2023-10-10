using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] Sprite alliedSprite;
    [SerializeField] Sprite enemySprite;
    [SerializeField] Sprite neutralSprite;


    private CapturePointStateEnum state = CapturePointStateEnum.NEUTRAL;
    private SpriteRenderer spriteRenderer;

    public CapturePointStateEnum State { get => state; }

    private void Awake()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetState(CapturePointStateEnum state)
    {
        this.state = state;
        spriteRenderer.sprite = state switch
        {
            CapturePointStateEnum.ALLIED => alliedSprite,
            CapturePointStateEnum.ENEMY => enemySprite,
            _ => neutralSprite,
        };
    }
}
