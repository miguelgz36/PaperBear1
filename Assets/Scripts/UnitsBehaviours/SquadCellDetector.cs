using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadCellDetector : MonoBehaviour
{
    private Cell cell;

    public Cell CurrentCell
    {
        get => cell; set => cell = value;
    }

    private void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Cell cell = collision.gameObject.GetComponent<Cell>();
        if (cell)
        {
            this.cell = cell;
        }
    }


     
}
