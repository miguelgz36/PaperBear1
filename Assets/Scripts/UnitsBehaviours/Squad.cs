using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using System.Linq;

public class Squad : MonoBehaviour
{
    List<GameObject> units;
    AlliedSquad placeable;
    [SerializeField] float movementSpeed = 1;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    private void Awake()
    {
        units = new List<GameObject>();
        placeable = GetComponent<AlliedSquad>();
        InitUnits();
        CommandUtils.InitSquadActions<Action>(gameObject, AppDomain.CurrentDomain.GetAssemblies());
    }

    private void InitUnits()
    {
        foreach (UnitController unit in GetComponentsInChildren<UnitController>())
        {
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

    public void SetCell(Cell cell)
    {
        foreach (GameObject unit in units)
        {
            unit.GetComponentInChildren<Health>().CurrentCell = cell;
        }
    }

    public T AddActionComponent<T>() where T : Component, Action
    {
        return gameObject.AddComponent<T>();
    }

    public void ExecuteAction<T>(Dictionary<CommandParamEnum, object> args) where T: Component, Action
    {
        Action action = gameObject.GetComponent<T>();
        if (action != null)
        {
            action.Execute(args);
        }
    }
}   
