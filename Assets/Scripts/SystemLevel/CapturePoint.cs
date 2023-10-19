using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] Sprite alliedSprite;
    [SerializeField] Sprite enemySprite;
    [SerializeField] Sprite neutralSprite;

    [SerializeField] Sprite alliedSpriteFlag;
    [SerializeField] Sprite enemySpriteFlag;
    [SerializeField] Sprite neutralSpriteFlag;

    private CapturePointStateEnum state = CapturePointStateEnum.NEUTRAL;
    private SpriteRenderer spriteRenderer;
    private Image imageUI;


    public CapturePointStateEnum State { get => state; }
    public Image SpriteUI { set => imageUI = value; }

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

        imageUI.sprite = state switch
        {
            CapturePointStateEnum.ALLIED => alliedSpriteFlag,
            CapturePointStateEnum.ENEMY => enemySpriteFlag,
            _ => neutralSpriteFlag,
        };
    }
}
