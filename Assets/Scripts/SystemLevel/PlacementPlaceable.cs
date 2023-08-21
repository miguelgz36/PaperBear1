using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementPlaceable : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    [SerializeField] Image cooldownImage;
    [SerializeField] TMPro.TextMeshProUGUI capText;

    private PlaceableCooldown squadCooldown;
    private Placeable placeable;
    private Button button;

    private void Awake()
    {
        placeable = unitPrefab.GetComponent<Placeable>();
        button = GetComponentInParent<Button>();
        squadCooldown = new PlaceableCooldown(placeable);
    }

    private void Start()
    {
        squadCooldown.Start();
    }


    private void Update()
    {
        squadCooldown.Update();
        cooldownImage.fillAmount = 1 - ((1 * squadCooldown.CurrentTime) / placeable.CoolDown);
        if (squadCooldown.CurrentTime < placeable.CoolDown || (squadCooldown.CurrentPopulation >= placeable.MaxCapPopulation && !squadCooldown.IsInfiniteCap))
        {
            button.interactable = false;
        } 
        else if (squadCooldown.CurrentTime >= placeable.CoolDown)
        {
            button.interactable = true;
        }
        capText.text = squadCooldown.TextButton;
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

    public void ResetCooldown(GameObject unitSpawned)
    {
        Placeable placeable = unitSpawned.GetComponent<Placeable>();
        placeable.PlaceableCooldown = squadCooldown;
        cooldownImage.fillAmount = 1f;
        button.interactable = false;
        squadCooldown.ResetCooldown();
    }

    public bool CapValid()
    {
        return squadCooldown.CapValid();
    }
}
