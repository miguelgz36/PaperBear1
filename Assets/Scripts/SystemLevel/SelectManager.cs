using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : Singleton<SelectManager>
{
    private GameObject selectedObjectToPlace;
    private PlayerControls playerControls;
    private Selectable objectReadyToSelect;
    private Selectable objectSelected;
    private PlacementPlaceable placementButton;
    private GameObject lastSpawned;

    public Selectable SquadReadyToSelect { get => objectReadyToSelect; set => objectReadyToSelect = value; }

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
        playerControls.Mouse.Select.started += _ => Select();
    }
    private void Select()
    {
        if (SquadReadyToSelect)
        {
            if (objectSelected && objectReadyToSelect != objectSelected)
            {
                objectSelected.SetSelectionUI(false);
            }
            objectSelected = objectReadyToSelect;
            objectSelected.SetSelectionUI(true);
        }
        else
        {
            if (selectedObjectToPlace != null && SquadReadyToSelect == null)
            {
                PlacePlaceable();
            }



            selectedObjectToPlace = null;
            placementButton = null;
            PlaceableCells.Instance.HidePlaceableZones();
            if (objectSelected)
            {
                objectSelected.SetSelectionUI(false);
            }
        }

    }

    private void PlacePlaceable()
    {
        Vector3 inputMouse = Input.mousePosition;
        Vector3 positionToPlace = Camera.main.ScreenToWorldPoint(inputMouse);
        positionToPlace.z = 0;
        PlaceableZone placeableZone = MouseFollower.Instance.PlaceableZoneToSelect;

        Artillery artillery = selectedObjectToPlace.GetComponent<Artillery>();
        DronLauncher dronLauncher = selectedObjectToPlace.GetComponent<DronLauncher>();
        AlliedSquad alliedSquad = selectedObjectToPlace.GetComponent<AlliedSquad>();
        bool placed = false;
        if (artillery && MouseFollower.Instance.InteractableToSelect)
        {
            PlaceArtillery(positionToPlace);
            placed = true;
        }
        else if (artillery)
        {
            artillery.DeactivedPreviewExplosion();
        }
        if (dronLauncher && MouseFollower.Instance.PlaceableZoneToSelect)
        {
            SendDron(dronLauncher);
            placed = true;
        }
        if (alliedSquad && placeableZone != null
                                   && placeableZone.ObjectInZone == null && placementButton.CapValid())
        {
            PlaceUnit(positionToPlace);
            placed = true;
        }
        if (placed && placementButton && lastSpawned)
        {
            placementButton.ResetCooldown(lastSpawned);
        }
    }

    private void PlaceUnit(Vector3 positionToPlace)
    {
        positionToPlace.y = (Mathf.Floor(positionToPlace.y / 4f) * 4f) + 2f;
        positionToPlace.x = (Mathf.Floor(positionToPlace.x / 4f) * 4f) + 2f;
        lastSpawned = Instantiate(selectedObjectToPlace, positionToPlace, Quaternion.Euler(0, 0, -90));
        MouseFollower.Instance.PlaceableZoneToSelect.ObjectInZone = lastSpawned;
    }

    private void PlaceArtillery(Vector3 positionToPlace)
    {
        GameObject artilleryInsantiate = Instantiate(selectedObjectToPlace, SupportFireManager.Instance.PositionAlliedSupportingFire.transform.position, Quaternion.identity);
        Artillery artillery = artilleryInsantiate.GetComponent<Artillery>();
        artillery.FireShells(positionToPlace, SupportFireManager.Instance.PositionAlliedSupportingFire.transform.position);
    }

    public void SetUnitToPlaceSquad(GameObject selected, PlacementPlaceable placementButton)
    {
        selectedObjectToPlace = selected;
        this.placementButton = placementButton;
        Artillery artillery = selected.GetComponent<Artillery>();
        if (artillery)
        {
            artillery.ActivedPreviewExplosion();
        }
    }

    private void SendDron(DronLauncher dronLauncher)
    {
        dronLauncher.DeployDron(MouseFollower.Instance.PlaceableZoneToSelect.transform.position, SupportFireManager.Instance.PositionAlliedSupportingFire.transform.position);
    }

}
