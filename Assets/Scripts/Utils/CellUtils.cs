using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public static class CellUtils
    {
        public static Cell GetNextCell(Cell currentCell, Vector3 direction)
        {
            Cell nextCell = null;

            RaycastHit2D[] ray = Physics2D.RaycastAll(currentCell.gameObject.transform.position, direction, LayerMask.GetMask("Cell"));

            int index = 0;

            while (index < ray.Length && nextCell == null)
            {
                Cell possibleNextCell = ray[index].collider.gameObject.GetComponent<Cell>();
                if (possibleNextCell && nextCell == null && possibleNextCell != currentCell)
                {
                    nextCell = possibleNextCell;
                }
                index++;
            }

            return nextCell;
        }
    }
}