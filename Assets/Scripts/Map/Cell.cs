using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] int coverage = 0;
    [Range(0f, 1f)]
    [SerializeField] float speed = 1f;
    private Squad squadInCell = null;
    private Squad futureSquadInCell = null;
    private Structure structure;

    public Squad SquadInCell { get => squadInCell; set => squadInCell = value; }
    public Squad FutureSquadInCell { get => futureSquadInCell; set => futureSquadInCell = value; }
    public Structure Structure { get => structure; set => structure = value; }

    public bool hasStructure()
    {
        return structure != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SquadCellDetector squadCellDectector = collision.GetComponent<SquadCellDetector>();

        if (squadCellDectector)
        {
            Debug.Log("squad in cell");
            this.SquadInCell = squadCellDectector.GetComponentInParent<Squad>();
            this.futureSquadInCell = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SquadCellDetector squadCellDectector = collision.GetComponent<SquadCellDetector>();

        if (squadCellDectector)
        {
            if(this.squadInCell == squadCellDectector.GetComponentInParent<Squad>())
            {
                this.squadInCell = null;
                this.futureSquadInCell = null;
            }
        }
    }

    public bool RejectProjectile()
    {
        return Random.Range(0, 100) <= coverage;
    }

    public bool IsAvailable()
    {
        return squadInCell == null && futureSquadInCell == null;
    }
}
