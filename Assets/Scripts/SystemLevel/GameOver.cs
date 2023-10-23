using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] List<Vector2> cellsObjective;
    [SerializeField] GameObject prefabCapturePoint;
    [SerializeField] List<Image> uiSpriteRender;

    private List<Cell> objectives;
    private Map map;

    private void Start()
    {
        objectives = new();
        map = FindAnyObjectByType<Map>();
        int i = 0;
        foreach (Vector2 vector2 in cellsObjective)
        {
            int x = ((int)vector2.x);
            int y = ((int)vector2.y);
            Cell cell = map.MatrixCell[x][y];
            GameObject capturePoint = Instantiate(prefabCapturePoint, cell.gameObject.transform);
            cell.CapturePoint = capturePoint.GetComponent<CapturePoint>();
            objectives.Add(cell);
            capturePoint.GetComponent<CapturePoint>().SpriteUI = uiSpriteRender[i];
            i++;
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
