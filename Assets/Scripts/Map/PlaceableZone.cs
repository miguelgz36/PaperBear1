using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableZone : MonoBehaviour
{
    [SerializeField] private Sprite spriteSelector;

    private GameObject objectInZone;
    private SpriteRenderer spriteRenderer;
    private Sprite startSprite;

    public GameObject ObjectInZone { get => objectInZone; set => objectInZone = value; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        startSprite = spriteRenderer.sprite;
    }

    public void ShowPlaceableZone()
    {
        spriteRenderer.enabled = true;
    }

    public void HidePlaceableZone()
    {
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MouseFollower>())
        {
            spriteRenderer.sprite = spriteSelector;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MouseFollower>())
        {
            spriteRenderer.sprite = startSprite;
        }

        if (collision.GetComponent<SquadCellDetector>())
        {
            this.objectInZone = null;
        }
    }
}
