using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInstructionMoveUnit : IAInstruction
{
    public IAInstructionMoveUnit(IA ia) : base(ia)
    {

    }
    public override bool Execute()
    {
        int countSquads = ia.SquadsSpawned.Count;
        if (countSquads == 0)
        {
            return false;
        }
        int indexUnitToMove = Random.Range(0, countSquads);
        Squad squadToMove = ia.SquadsSpawned[indexUnitToMove];

        if (squadToMove == null)
        {
            ia.SquadsSpawned.RemoveAt(indexUnitToMove);
            return false;
        }

        Cell cellToMove = squadToMove.GetComponentInChildren<SquadCellDetector>().CurrentCell.GetNextCell(-1);
        if (cellToMove == null) return false;

        Dictionary<CommandParamEnum, object> args = new();
        args.Add(CommandParamEnum.SQUAD, squadToMove);
        args.Add(CommandParamEnum.CELL_TO_MOVE, cellToMove);

        return squadToMove.ExecuteAction<ActionMoveTo>(args);
    }
}
