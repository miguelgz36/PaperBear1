using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    List<GameObject> units;
    private void Awake()
    {
        units = new List<GameObject>();
        foreach (UnitController unit in GetComponentsInChildren<UnitController>()) {
            units.Add(unit.gameObject);
        }
    }

    private void Update()
    {
        if(units.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    public void RemoveUnit(GameObject gameObject)
    {
        units.Remove(gameObject);
    }
}
