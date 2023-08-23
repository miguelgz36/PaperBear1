using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] int width = 15;
    [SerializeField] int height = 7;
    Cell[][] matrixCell;

    public int Width { get => width; }
    public int Height { get => height; }
    public Cell[][] MatrixCell { get => matrixCell; }

    private void Awake()
    {
        matrixCell = new Cell[width][];
        Cell[] cells = GetComponentsInChildren<Cell>();
        int x = 0;
        int y = 0;
        matrixCell[x] = new Cell[width];
        for (int i = 0; i < cells.Length; i++)
        {
            if(y == 0) matrixCell[x] = new Cell[height];

            Cell cell = cells[i];

            matrixCell[x][y] = cell;

            x++;

            if(x == width)
            {
                x = 0;
                y++;
                if (y == height)
                {
                    break;
                }         
            }           
        }
    }



    public Cell ReturnFirstCellWith<T>()
    {
        int numberCells = width * height;
        int currentCellsChecked = 0;
        for(int x = Random.Range(0, width); x < width; x++)
        {
            for(int y = Random.Range(0, height); y < height; y++)
            {
                Cell cell = matrixCell[x][y];
                if (cell.Contains<T>())
                {
                    return cell;
                }
                currentCellsChecked++;
                if (currentCellsChecked == numberCells)
                {
                    break;
                }
                if (y == height)
                {
                    y = 0;
                }
            }
        }

        return null;
    }
}
