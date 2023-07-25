using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementButton : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    private Placeable placeable;
    private Button button;
    private float currentCooldown;

    private void Awake()
    {
        placeable = unitPrefab.GetComponent<Placeable>();
        button = GetComponentInParent<Button>();
    }

    private void Start()
    {
        currentCooldown = 0;
    }


    private void Update()
    {
        if (!button.interactable)
        {
            currentCooldown += (Time.deltaTime);
            Debug.Log(currentCooldown);
        }   
        if (currentCooldown < placeable.BasicCost)
        {
            button.interactable = false;
        } else if (currentCooldown >= placeable.BasicCost )
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

        if(placeable is DronLauncher)
        {
            PlaceableCells.Instance.ShowPlaceableZones();
        }

        SelectManager.Instance.SetUnitToPlaceSquad(unitPrefab, this);
    }

    public void ResetCooldown()
    {
        currentCooldown = 0f;
    }
}
