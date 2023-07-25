using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementButton : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    [SerializeField] Image cooldownImage;
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
            cooldownImage.fillAmount = 1 - ((1 * currentCooldown) / placeable.CoolDown);
        }   
        if (currentCooldown < placeable.CoolDown)
        {
            button.interactable = false;
        } else if (currentCooldown >= placeable.CoolDown )
        {
            button.interactable = true;
            Debug.Log("Inerectable");

        }
    }
    public void SetCurrentUnitToPlace()
    {
        Debug.Log("SelectUnit");
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
        cooldownImage.fillAmount = 1f;
        currentCooldown = 0f;
        button.interactable = false;
    }
}
