using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableCells : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private void Awake()
    {
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
