using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private GameObject selectedObject;
    private PlayerControls playerControls;
    private Resources resources;
    private Selector selector;

    private void Awake()
    {
        playerControls = new PlayerControls();
        resources = FindAnyObjectByType<Resources>();
        selector = FindAnyObjectByType<Selector>();
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
        if (selectedObject != null && resources.CurrentResources >= selectedObject.GetComponent<AlliedSquad>().BasicCost 
            && selector.PlaceableZoneToSelect && selector.PlaceableZoneToSelect.ObjectInZone == null)
        {
            Vector3 positionToPlace = Camera.main.ScreenToWorldPoint(inputMouse);
            positionToPlace.z = 0;
            positionToPlace.y = (Mathf.Floor(positionToPlace.y / 4f) * 4f) + 2f;
            positionToPlace.x = (Mathf.Floor(positionToPlace.x / 4f) * 4f) + 2f;
            GameObject instance = Instantiate(selectedObject, positionToPlace, Quaternion.Euler(0, 0, -90));
            resources.CurrentResources -= selectedObject.GetComponent<AlliedSquad>().BasicCost;
            selector.PlaceableZoneToSelect.ObjectInZone = instance;
        }
        selectedObject = null;
    }
    public void SetUnitToPlace(GameObject selected)
    {
        selectedObject = selected;
    }
}
