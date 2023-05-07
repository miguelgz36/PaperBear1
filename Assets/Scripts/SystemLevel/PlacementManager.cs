using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private GameObject selectedObject;
    private Playercontrols playerControls;

    private void Awake()
    {
        playerControls = new Playercontrols();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Build.PlaceUnit.started += _ => PlaceUnit(); 
    }
    private void PlaceUnit()
    {
        Debug.Log("PLACE");
        if(selectedObject != null)
        {
            Vector3 positionToPlace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionToPlace.z = 0;
            Instantiate(selectedObject, positionToPlace, Quaternion.Euler(0, 0, -90));
            selectedObject = null;
        }
    }
    public void SetUnitToPlace(GameObject selected)
    {
        selectedObject = selected;
    }
}
