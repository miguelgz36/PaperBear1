using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableCells : Singleton<PlaceableCells>
{
    private TilemapRenderer tilemapRenderer;
    protected override void Awake()
    {
        base.Awake();
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    public void ShowPlaceableZone()
    {
        tilemapRenderer.enabled = true;
    }

    public void HidePlaceableZone()
    {
        tilemapRenderer.enabled = false;
    }
}
