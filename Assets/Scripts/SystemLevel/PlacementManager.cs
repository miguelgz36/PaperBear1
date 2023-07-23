using System;
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
        if(selectedObject != null)
        {
            Vector3 inputMouse = Input.mousePosition;
            Vector3 positionToPlace = Camera.main.ScreenToWorldPoint(inputMouse);
            positionToPlace.z = 0;

            Artillery artillery = selectedObject.GetComponent<Artillery>();
            AlliedSquad alliedSquad = selectedObject.GetComponent<AlliedSquad>();
            DronLauncher dronLauncher = selectedObject.GetComponent<DronLauncher>();
            if (artillery)
            {
                FireArtillery(artillery, positionToPlace);
            }
            if(alliedSquad)
            {
                PlaceAlliedSquad(positionToPlace);
            }
            if (dronLauncher)
            {
                SendDron(dronLauncher);
            }
        }
        
        selectedObject = null;
        PlaceableCells.Instance.HidePlaceableZones();
    }

    private void FireArtillery(Artillery artillery, Vector3 positionToPlace)
    {
        if (Selector.Instance.InteractableToSelect)
        {
            GameObject artilleryInsantiate = Instantiate(selectedObject, SupportFireManager.Instance.PositionAlliedSupportingFire.transform.position, Quaternion.identity);
            artillery = artilleryInsantiate.GetComponent<Artillery>();
            artillery.FireShells(positionToPlace);
        }
        else
        {
            artillery.DeactivedPreviewExplosion();
        }
    }



    private void PlaceAlliedSquad(Vector3 positionToPlace)
    {
        if (Resources.Instance.CurrentResources >= selectedObject.GetComponent<AlliedSquad>().BasicCost && Selector.Instance.PlaceableZoneToSelect
           && Selector.Instance.PlaceableZoneToSelect.ObjectInZone == null)
        {
            positionToPlace.y = (Mathf.Floor(positionToPlace.y / 4f) * 4f) + 2f;
            positionToPlace.x = (Mathf.Floor(positionToPlace.x / 4f) * 4f) + 2f;
            GameObject instance = Instantiate(selectedObject, positionToPlace, Quaternion.Euler(0, 0, -90));
            Resources.Instance.CurrentResources -= selectedObject.GetComponent<AlliedSquad>().BasicCost;
            Selector.Instance.PlaceableZoneToSelect.ObjectInZone = instance;
        }
    }
    
    public void SetUnitToPlaceSquad(GameObject selected)
    {
        selectedObject = selected;
        Artillery artillery = selected.GetComponent<Artillery>();
        if (artillery)
        {
            artillery.ActivedPreviewExplosion();
        }
    }

    private void SendDron(DronLauncher dronLauncher)
    {
        dronLauncher.DeployDron(Selector.Instance.PlaceableZoneToSelect.transform.position);
    }

}
