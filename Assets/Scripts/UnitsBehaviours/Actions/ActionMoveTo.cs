using Assets.Scripts.Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMoveTo : MonoBehaviour, IAction
{

    private Squad squad;
    private Cell cellToMove;
    private Cell nextCell;
    private Cell currentCell;
    private bool isMoving = false;
    private Vector3 target;
    private float proximityThreshold = 0.1f;

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (Vector3.Distance(squad.gameObject.transform.position, cellToMove.gameObject.transform.position) < proximityThreshold)
            {
                isMoving = false;
                this.squad.IsBusy = false;
            }
            else if (Vector3.Distance(squad.gameObject.transform.position, target) < proximityThreshold)
            {
                nextCell = currentCell.GetNextCell((int)squad.gameObject.transform.up.normalized.x);
                if (nextCell == null && !nextCell.IsAvailable())
                {
                    isMoving = false;
                    squad.IsBusy = false;
                }
                else
                {
                    nextCell.FutureSquadInCell = this.squad;
                    this.target = nextCell.gameObject.transform.position;
                }
            }
            else
            {
                this.squad.gameObject.transform.position += this.squad.MovementSpeed * Time.fixedDeltaTime * this.squad.gameObject.transform.up;
            }
        }
    }


    public bool Execute(Dictionary<CommandParamEnum, object> args)
    {
        squad = (Squad)args.GetValueOrDefault(CommandParamEnum.SQUAD);
        currentCell = squad.GetComponentInChildren<SquadCellDetector>().CurrentCell;
        cellToMove = (Cell)args.GetValueOrDefault(CommandParamEnum.CELL_TO_MOVE);
        nextCell = currentCell.GetNextCell((int)squad.gameObject.transform.up.normalized.x);
        if (currentCell == cellToMove || isMoving || nextCell == null || !nextCell.IsAvailable()) return false;
        squad.IsBusy = true;
        isMoving = true;
        target = nextCell.gameObject.transform.position;

        return true;
    }
}
