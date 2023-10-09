using Assets.Scripts.Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMoveTo : MonoBehaviour, IAction
{

    private Squad squad;
    private Cell cellToMove;
    private Cell nextCell;
    private bool isMoving = false;
    private readonly float proximityThreshold = 0.1f;

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Cell currentCell = squad.GetComponentInChildren<SquadCellDetector>().CurrentCell;
            if (cellToMove != null && Vector3.Distance(squad.gameObject.transform.position, cellToMove.gameObject.transform.position) < proximityThreshold)
            {
                StopMoving();
            }
            else if (Vector3.Distance(squad.gameObject.transform.position, nextCell.gameObject.transform.position) < proximityThreshold)
            {
                if (cellToMove == null) StopMoving();
                nextCell = currentCell.GetNextCell((int)squad.gameObject.transform.up.normalized.x);
                if(nextCell != null && nextCell.IsAvailable(squad))
                {
                    currentCell.FutureSquadInCell = null;
                    nextCell.FutureSquadInCell = this.squad;
                }
                else
                {
                    StopMoving();
                }
            }
            else if(nextCell.IsAvailable(squad))
            {
                this.squad.gameObject.transform.position += (this.squad.MovementSpeed * currentCell.Speed) * Time.fixedDeltaTime * this.squad.gameObject.transform.up;
            }
            else
            {
                StopMoving();
            }
        }
    }

    public void StopMoving()
    {
        isMoving = false;
        squad.IsBusy = false;
        squad.IsMoving = false;
        nextCell = null;
        cellToMove = null;
    }

    public bool Execute(Dictionary<CommandParamEnum, object> args)
    {
        squad = (Squad)args.GetValueOrDefault(CommandParamEnum.SQUAD);
        Cell currentCell = squad.GetComponentInChildren<SquadCellDetector>().CurrentCell;
        cellToMove = (Cell)args.GetValueOrDefault(CommandParamEnum.CELL_TO_MOVE);
        nextCell = currentCell.GetNextCell((int)squad.gameObject.transform.up.normalized.x);
        if (currentCell == cellToMove || squad.IsBusy || isMoving || nextCell == null || !nextCell.IsAvailable(squad)) return false;
        squad.IsBusy = true;
        isMoving = true;
        squad.IsMoving = true;

        return true;
    }

    public void Stop()
    {
        if(squad && squad.IsMoving)
        {
            cellToMove = null;
        }
        
    }
}
