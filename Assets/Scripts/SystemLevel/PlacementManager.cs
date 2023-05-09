using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private GameObject selectedObject;
    private PlayerControls playerControls;

    private void Awake()
    {
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
        if(selectedObject != null)
        {
            Vector3 positionToPlace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionToPlace.z = 0;
            positionToPlace.y = (Mathf.Floor(positionToPlace.y / 4f) * 4f) + 2f;
            positionToPlace.x = (Mathf.Floor(positionToPlace.x / 4f) * 4f) + 2f;
            Instantiate(selectedObject, positionToPlace, Quaternion.Euler(0, 0, -90));
            selectedObject = null;
        }
    }
    public void SetUnitToPlace(GameObject selected)
    {
        selectedObject = selected;
    }
}
