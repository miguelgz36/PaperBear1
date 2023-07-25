using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : Singleton<SelectManager>
{
    private GameObject selectedObjectToPlace;
    private PlayerControls playerControls;
    private Selectable objectReadyToSelect;
    private Selectable objectSelected;
    private PlacementButton placementButton;

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
        playerControls.Build.Select.started += _ => Select();
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
        if (alliedSquad && Resources.Instance.CurrentResources >= alliedSquad.CoolDown && MouseFollower.Instance.PlaceableZoneToSelect != null
                                   && MouseFollower.Instance.PlaceableZoneToSelect.ObjectInZone == null)
        {
            PlaceUnit(positionToPlace);
            placed = true;
        }
        if (placed && placementButton)
        {
            placementButton.ResetCooldown();
        }
    }

    private void PlaceUnit(Vector3 positionToPlace)
    {
        positionToPlace.y = (Mathf.Floor(positionToPlace.y / 4f) * 4f) + 2f;
        positionToPlace.x = (Mathf.Floor(positionToPlace.x / 4f) * 4f) + 2f;
        GameObject instance = Instantiate(selectedObjectToPlace, positionToPlace, Quaternion.Euler(0, 0, -90));
        Resources.Instance.CurrentResources -= selectedObjectToPlace.GetComponent<AlliedSquad>().CoolDown;
        MouseFollower.Instance.PlaceableZoneToSelect.ObjectInZone = instance;
    }

    private void PlaceArtillery(Vector3 positionToPlace)
    {
        GameObject artilleryInsantiate = Instantiate(selectedObjectToPlace, SupportFireManager.Instance.PositionAlliedSupportingFire.transform.position, Quaternion.identity);
        Artillery artillery = artilleryInsantiate.GetComponent<Artillery>();
        artillery.FireShells(positionToPlace);
    }

    public void SetUnitToPlaceSquad(GameObject selected, PlacementButton placementButton)
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
        dronLauncher.DeployDron(MouseFollower.Instance.PlaceableZoneToSelect.transform.position);
    }

}
