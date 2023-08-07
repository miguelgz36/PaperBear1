using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Misc;

public class MoveToNextCell: MonoBehaviour, Action
{
    private Squad squad = null;

    private bool isMoving = false;

    private Vector3 target;

    private float proximityThreshold = 0.1f;

    void Awake()
    {
    }

    void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(squad.gameObject.transform.position, target) < proximityThreshold)
            {
                isMoving = false;
            }
            else
            {
                this.squad.gameObject.transform.position += this.squad.MovementSpeed * Time.deltaTime * this.squad.gameObject.transform.up;
            }
        }
    }

    public void Execute(Dictionary<CommandParamEnum, object> args)
    {
        this.squad = (Squad) args.GetValueOrDefault(CommandParamEnum.SQUAD);
        Cell currentCell = this.squad.GetComponentInChildren<SquadCellDetector>().CurrentCell;
        Cell nextCell = CellUtils.GetNextCell(currentCell, currentCell.SquadInCell.gameObject.transform.up);
        if (nextCell != null && nextCell.IsAvailable())
        {
            nextCell.FutureSquadInCell = this.squad;
            this.target = nextCell.gameObject.transform.position;

            isMoving = true;
        }
    }
}
