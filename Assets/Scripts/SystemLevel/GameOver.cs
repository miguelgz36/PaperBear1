using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] List<Vector2> cellsObjective;
    [SerializeField] GameObject prefabCapturePoint;
    
    private List<Cell> objectives;
    private Map map;

    private void Start()
    {
        objectives = new();
        map = FindAnyObjectByType<Map>();
        foreach (Vector2 vector2 in cellsObjective)
        {
            int x = ((int)vector2.x);
            int y = ((int)vector2.y);
            Cell cell = map.MatrixCell[x][y];
            GameObject capturePoint = Instantiate(prefabCapturePoint, cell.gameObject.transform);
            cell.CapturePoint = capturePoint.GetComponent<CapturePoint>();
            objectives.Add(cell);
        }
    }

    private void Update()
    {
        int numberOfEnemiesInObjective = 0;
        int numberOfAlliedsInObjective = 0;
 
        foreach (Cell cell in objectives)
        {      
            CapturePoint capturePoint = cell.gameObject.GetComponentInChildren<CapturePoint>();
            if (capturePoint.State.Equals(CapturePointStateEnum.ALLIED))
            {
                numberOfAlliedsInObjective++;
            }
            else if (capturePoint.State.Equals(CapturePointStateEnum.ENEMY))
            {
                numberOfEnemiesInObjective++;
            }
        }
        if (numberOfAlliedsInObjective == objectives.Count)
        {
            LevelStateManager.Instance.Win();
        }
        if (numberOfEnemiesInObjective == objectives.Count)
        {
            LevelStateManager.Instance.Lose();
        }
    }

}
