using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementButton : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    private AlliedSquad alliedSquad;
    private Button button;

    private void Awake()
    {
        alliedSquad = unitPrefab.GetComponent<AlliedSquad>();
        button = GetComponentInParent<Button>();
    }
 

    private void Update()
    {
        if (Resources.Instance.CurrentResources < alliedSquad.BasicCost)
        {
            button.interactable = false;
        } else if (Resources.Instance.CurrentResources >= alliedSquad.BasicCost )
        {
            button.interactable = true;
        }
    }
    public void SetCurrentUnitToPlace()
    {
        PlaceableCells.Instance.ShowPlaceableZones();
        PlacementManager.Instance.SetUnitToPlace(unitPrefab);
    }
}
