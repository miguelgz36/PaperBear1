using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : Singleton<OrderManager>
{
    private PlayerControls playerControls;

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
        playerControls.Mouse.Order.started += _ => Order();
    }

    private void Order()
    {
        if (SelectManager.Instance.ObjectSelected && MouseFollower.Instance.MouseInCell)
        {
            ExecuteMoveTo();
        }
    }

    private void ExecuteMoveTo()
    {
        Squad squad = SelectManager.Instance.ObjectSelected.GetComponent<Squad>();
        Cell currentSquadCell = squad.GetComponentInChildren<SquadCellDetector>().CurrentCell;
        Cell targetCell = MouseFollower.Instance.MouseInCell;

        if (!squad.IsBusy && targetCell.SquadIsInSameRow(squad) && targetCell.X > currentSquadCell.X)
        {
            Dictionary<CommandParamEnum, object> args = new();
            args.Add(CommandParamEnum.SQUAD, squad);
            args.Add(CommandParamEnum.CELL_TO_MOVE, targetCell);
            squad.ExecuteAction<ActionMoveTo>(args);
        }
    }

}
