using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : ActionButton
{
    public override void execute()
    {
        Dictionary<CommandParamEnum, object> args = new Dictionary<CommandParamEnum, object>();
        Cell cellToMove = this.squad.GetComponentInChildren<SquadCellDetector>().CurrentCell.GetNextCell(1);
        args.Add(CommandParamEnum.SQUAD, this.squad);
        args.Add(CommandParamEnum.CELL_TO_MOVE, cellToMove);
        this.squad.ExecuteAction<ActionMoveTo>(args);
    }
}
