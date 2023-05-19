using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableCells : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private List<PlaceableZone> placeableZones;
    private void Awake()
    {
        placeableZones = new List<PlaceableZone>(GetComponentsInChildren<PlaceableZone>());
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    public void ShowPlaceableZones()
    {
        tilemapRenderer.enabled = true;
        foreach(PlaceableZone placeableZone in placeableZones)
        {
            placeableZone.ShowPlaceableZone();
        }
    }

    public void HidePlaceableZones()
    {
        tilemapRenderer.enabled = false;
        foreach (PlaceableZone placeableZone in placeableZones)
        {
            placeableZone.HidePlaceableZone();
        }
    }
}
