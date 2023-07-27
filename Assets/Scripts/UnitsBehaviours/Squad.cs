using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    List<GameObject> units;
    AlliedSquad placeable;

    private void Awake()
    {
        units = new List<GameObject>();
        placeable = GetComponent<AlliedSquad>();
        foreach (UnitController unit in GetComponentsInChildren<UnitController>()) {
            units.Add(unit.gameObject);
        }
    }

    private void Update()
    {
        if (units.Count == 0)
        {
            if(placeable) placeable.ReducePopulation();
            Destroy(gameObject);
        }
    }

    public void RemoveUnit(GameObject gameObject)
    {
        units.Remove(gameObject);
    }
}
