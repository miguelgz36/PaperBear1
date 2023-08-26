using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using System.Linq;

public class Squad : MonoBehaviour
{
    List<UnitController> units;
    Placeable placeable;
    [SerializeField] float movementSpeed = 1;
    private Boolean isBusy = false;
    private int unitsCount;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public bool IsBusy { get => isBusy; set => isBusy = value; }


    private void Awake()
    {
        units = new List<UnitController>();
        placeable = GetComponent<Placeable>();
        InitUnits();
        CommandUtils.InitSquadActions<IAction>(gameObject, AppDomain.CurrentDomain.GetAssemblies());
    }

    private void InitUnits()
    {
        foreach (UnitController unit in GetComponentsInChildren<UnitController>())
        {
            units.Add(unit);
        }
        unitsCount = units.Count;
    }

    private void Update()
    {
        if (unitsCount <= 0)
        {
            placeable.ReducePopulation();
            Destroy(gameObject);
        }
    }

    public void RemoveUnit(UnitController gameObject)
    {
        units.Remove(gameObject);
        unitsCount--;
    }

    public void SetCell(Cell cell)
    {
        foreach (UnitController unit in units)
        {
            unit.GetComponentInChildren<Health>().CurrentCell = cell;
        }
    }

    public T AddActionComponent<T>() where T : Component, IAction
    {
        return gameObject.AddComponent<T>();
    }

    public bool ExecuteAction<T>(Dictionary<CommandParamEnum, object> args) where T: Component, IAction
    {
        IAction action = gameObject.GetComponent<T>();
        if (action != null)
        {
            return action.Execute(args);
        }
        return false;
    }

    public void AimTarget(Collider2D collider2D)
    {
        foreach (UnitController unit in units)
        {
            unit.AimTarget(collider2D);
        }
    }
    public void UnAimTarget(Collider2D collider2D)
    {
        foreach (UnitController unit in units)
        {
            unit.UnAimTarget(collider2D);
        }
    }

}   
