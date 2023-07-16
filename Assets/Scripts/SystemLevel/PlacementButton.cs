using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementButton : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    private Placeable placeable;
    private Button button;

    private void Awake()
    {
        placeable = unitPrefab.GetComponent<Placeable>();
        button = GetComponentInParent<Button>();
    }
 

    private void Update()
    {
        if (Resources.Instance.CurrentResources < placeable.BasicCost)
        {
            button.interactable = false;
        } else if (Resources.Instance.CurrentResources >= placeable.BasicCost )
        {
            button.interactable = true;
        }
    }
    public void SetCurrentUnitToPlace()
    {
        if(placeable is AlliedSquad)
        {
            PlaceableCells.Instance.ShowPlaceableZones();
        }
        PlacementManager.Instance.SetUnitToPlaceSquad(unitPrefab);

    }
}
