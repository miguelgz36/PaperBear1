using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : Singleton<SelectManager>
{
    private GameObject selectedObjectToPlace;
    private PlayerControls playerControls;
    private AlliedSquad squadReadyToSelect;
    private AlliedSquad squadSelected;

    public AlliedSquad SquadReadyToSelect { get => squadReadyToSelect; set => squadReadyToSelect = value; }

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
            if (squadSelected && squadReadyToSelect != squadSelected)
            {
                squadSelected.SetSelectionUI(false);
            }
            squadSelected = SquadReadyToSelect;
            squadSelected.SetSelectionUI(true);
        }
        else
        {
            if (selectedObjectToPlace != null && SquadReadyToSelect == null)
            {
                PlacePlaceable();
            }

            selectedObjectToPlace = null;
            PlaceableCells.Instance.HidePlaceableZones();
            if (squadSelected)
            {
                squadSelected.SetSelectionUI(false);
            }
        }
        
    }

    private void PlacePlaceable()
    {
        Vector3 inputMouse = Input.mousePosition;
        Vector3 positionToPlace = Camera.main.ScreenToWorldPoint(inputMouse);
        positionToPlace.z = 0;

        Artillery artillery = selectedObjectToPlace.GetComponent<Artillery>();
        if (artillery)
        {
            PlaceArtillery(positionToPlace, artillery);
        }
        else
        {
            PlaceUnit(positionToPlace);
        }
    }

    private void PlaceUnit(Vector3 positionToPlace)
    {
        if (Resources.Instance.CurrentResources >= selectedObjectToPlace.GetComponent<AlliedSquad>().BasicCost && MouseFollower.Instance.PlaceableZoneToSelect
                                   && MouseFollower.Instance.PlaceableZoneToSelect.ObjectInZone == null)
        {
            positionToPlace.y = (Mathf.Floor(positionToPlace.y / 4f) * 4f) + 2f;
            positionToPlace.x = (Mathf.Floor(positionToPlace.x / 4f) * 4f) + 2f;
            GameObject instance = Instantiate(selectedObjectToPlace, positionToPlace, Quaternion.Euler(0, 0, -90));
            Resources.Instance.CurrentResources -= selectedObjectToPlace.GetComponent<AlliedSquad>().BasicCost;
            MouseFollower.Instance.PlaceableZoneToSelect.ObjectInZone = instance;
        }
    }

    private void PlaceArtillery(Vector3 positionToPlace, Artillery artillery)
    {
        if (MouseFollower.Instance.InteractableToSelect)
        {
            GameObject artilleryInsantiate = Instantiate(selectedObjectToPlace, SupportFireManager.Instance.PositionAlliedSupportingFire.position, Quaternion.identity);
            artillery = artilleryInsantiate.GetComponent<Artillery>();
            artillery.FireShells(positionToPlace);
        }
        else
        {
            artillery.DeactivedPreviewExplosion();
        }
    }

    public void SetUnitToPlaceSquad(GameObject selected)
    {
        selectedObjectToPlace = selected;
        Artillery artillery = selected.GetComponent<Artillery>();
        if (artillery)
        {
            artillery.ActivedPreviewExplosion();
        }
    }

}
