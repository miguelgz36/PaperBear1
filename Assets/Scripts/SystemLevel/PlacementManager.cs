using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : Singleton<PlacementManager>
{
    private GameObject selectedObject;
    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Build.PlaceUnit.started += _ => PlaceUnit(); 
    }
    private void PlaceUnit()
    {
        Vector3 inputMouse = Input.mousePosition;
        if (selectedObject != null && Resources.Instance.CurrentResources >= selectedObject.GetComponent<AlliedSquad>().BasicCost 
            && Selector.Instance.PlaceableZoneToSelect && Selector.Instance.PlaceableZoneToSelect.ObjectInZone == null)
        {
            Vector3 positionToPlace = Camera.main.ScreenToWorldPoint(inputMouse);
            positionToPlace.z = 0;
            positionToPlace.y = (Mathf.Floor(positionToPlace.y / 4f) * 4f) + 2f;
            positionToPlace.x = (Mathf.Floor(positionToPlace.x / 4f) * 4f) + 2f;
            GameObject instance = Instantiate(selectedObject, positionToPlace, Quaternion.Euler(0, 0, -90));
            Resources.Instance.CurrentResources -= selectedObject.GetComponent<AlliedSquad>().BasicCost;
            Selector.Instance.PlaceableZoneToSelect.ObjectInZone = instance;
        }
        selectedObject = null;
        PlaceableCells.Instance.HidePlaceableZones();
    }
    public void SetUnitToPlace(GameObject selected)
    {
        selectedObject = selected;
    }
}
