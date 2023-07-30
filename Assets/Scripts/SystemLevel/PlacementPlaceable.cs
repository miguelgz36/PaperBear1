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
    private Placeable placeable;
    private Button button;
    private float currentCooldown;
    private int currentPopulation;
    private bool isInfiniteCap;

    private void Awake()
    {
        placeable = unitPrefab.GetComponent<Placeable>();
        button = GetComponentInParent<Button>();
    }

    private void Start()
    {
        currentCooldown = 0;
        currentPopulation = 0;
        isInfiniteCap = placeable.MaxCapPopulation == 0;
        if (!isInfiniteCap)
        {
            SetTextCap();
        }
    }

    private void SetTextCap()
    {
        capText.text = currentPopulation + "/" + placeable.MaxCapPopulation.ToString();
    }

    private void Update()
    {
        if (!button.interactable)
        {
            currentCooldown += (Time.deltaTime);
            cooldownImage.fillAmount = 1 - ((1 * currentCooldown) / placeable.CoolDown);
        }   
        if (currentCooldown < placeable.CoolDown || (currentPopulation >= placeable.MaxCapPopulation && !isInfiniteCap))
        {
            button.interactable = false;
        } 
        else if (currentCooldown >= placeable.CoolDown)
        {
            button.interactable = true;
        }
        if (!isInfiniteCap)
        {
            SetTextCap();
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

    public void ResetCooldown(GameObject unitSpawned)
    {
        Placeable placeable = unitSpawned.GetComponent<Placeable>();
        placeable.OriginButton = this;
        currentPopulation++;
        cooldownImage.fillAmount = 1f;
        currentCooldown = 0f;
        button.interactable = false;
    }

    public void DecreasePopulation()
    {
        currentPopulation--;
    }

    internal bool CapValid()
    {
        return currentPopulation < placeable.MaxCapPopulation || isInfiniteCap;
    }
}
