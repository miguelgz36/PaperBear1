using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementButton : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    private PlacementManager placementManager;
    private void Awake()
    {
        placementManager = FindAnyObjectByType<PlacementManager>();
    }

    public void SetCurrentUnitToPlace()
    {
        placementManager.SetUnitToPlace(unitPrefab);
    }
}
