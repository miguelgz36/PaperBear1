using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float coverage = 0;
    [Range(0f, 1f)]
    [SerializeField] float speed = 1f;
    private Squad squadInCell = null;
    private Squad futureSquadInCell = null;

    public Squad SquadInCell { get => squadInCell; set => squadInCell = value; }
    public Squad FutureSquadInCell { get => futureSquadInCell; set => futureSquadInCell = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SquadCellDetector squadCellDectector = collision.GetComponent<SquadCellDetector>();

        if (squadCellDectector)
        {
            Squad squad = squadCellDectector.GetComponentInParent<Squad>();
            this.SquadInCell = squad;
            squad.SetCell(this);
            this.futureSquadInCell = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SquadCellDetector squadCellDectector = collision.GetComponent<SquadCellDetector>();

        if (squadCellDectector)
        {
            Squad squad = squadCellDectector.GetComponentInParent<Squad>();
            if (this.squadInCell == squad)
            {
                this.squadInCell = null;
                this.futureSquadInCell = null;
            }
        }
    }

    public float ReduceDamage(float damage)
    {
        return damage - (damage * coverage);
    }

    public bool IsAvailable()
    {
        return squadInCell == null && futureSquadInCell == null;
    }
}
