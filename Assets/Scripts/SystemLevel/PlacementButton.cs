using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementButton : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    private PlacementManager placementManager;
    private Resources resources;
    private AlliedSquad alliedSquad;
    private Button button;
    private PlaceableCells placeableCells;
    private void Awake()
    {
        placementManager = FindAnyObjectByType<PlacementManager>();
        resources = FindAnyObjectByType<Resources>();
        alliedSquad = unitPrefab.GetComponent<AlliedSquad>();
        button = GetComponentInParent<Button>();
        placeableCells = FindAnyObjectByType<PlaceableCells>();
    }
 

    private void Update()
    {
        if (resources.CurrentResources < alliedSquad.BasicCost)
        {
            button.interactable = false;
        } else if (resources.CurrentResources >= alliedSquad.BasicCost )
        {
            button.interactable = true;
        }
    }
    public void SetCurrentUnitToPlace()
    {
        placeableCells.ShowPlaceableZones();
        placementManager.SetUnitToPlace(unitPrefab);
    }
}
