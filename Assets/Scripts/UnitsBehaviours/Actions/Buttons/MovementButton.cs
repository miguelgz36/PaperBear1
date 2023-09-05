using System.Collections.Generic;

public class MovementButton : ActionButton
{
    public override void execute()
    {
        Dictionary<CommandParamEnum, object> args = new Dictionary<CommandParamEnum, object>();
        Cell cellToMove = this.currentSquad.GetComponentInChildren<SquadCellDetector>().CurrentCell.GetNextCell(1);
        args.Add(CommandParamEnum.SQUAD, this.currentSquad);
        args.Add(CommandParamEnum.CELL_TO_MOVE, cellToMove);
        this.currentSquad.ExecuteAction<ActionMoveTo>(args);
    }
}
