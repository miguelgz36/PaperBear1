using System;
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
    private Structure structure;
    private Map map;

    private int x;
    private int y;

    public Squad SquadInCell { get => squadInCell; set => squadInCell = value; }
    public Squad FutureSquadInCell { get => futureSquadInCell; set => futureSquadInCell = value; }
    public Structure Structure { get => structure; set => structure = value; }
    public int X { get => x; }
    public int Y { get => y; }

    public void Init(int x, int y, Map map)
    {
        this.x = x;
        this.y = y;
        this.map = map;
    }
  
    public bool SquadIsInSameRow(Squad squad)
    {
        return squad.GetComponentInChildren<SquadCellDetector>().CurrentCell.y == y;
    }

    public Cell GetNextCell(int direction)
    {
        int xNextCell = this.x + direction;
        if (xNextCell < 0 || xNextCell == map.Width) return null;

        return map.MatrixCell[xNextCell][y];
    }

    public bool HasStructure()
    {
        return structure != null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        SquadCellDetector squadCellDectector = collision.GetComponent<SquadCellDetector>();

        if (squadCellDectector)
        {
            Squad squad = squadCellDectector.GetComponentInParent<Squad>();
            if(squad != squadInCell)
            {
                this.SquadInCell = squad;
                squad.SetCell(this);
            }       
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
            }
        }
    }

    public bool Contains<T>()
    {
        return squadInCell != null && squadInCell.GetComponent<T>() != null;
    }

    public float ReduceDamage(float damage)
    {
        return damage - (damage * coverage);
    }

    public bool IsAvailable(Squad squad)
    {
        if (squadInCell == squad || futureSquadInCell == squad) return true;
        return squadInCell == null && futureSquadInCell == null;
    }
}
