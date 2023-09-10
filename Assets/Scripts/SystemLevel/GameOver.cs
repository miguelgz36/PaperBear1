using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] 
    private List<Vector2> cellsObjective;
    
    List<Cell> objectives;
    Map map;

    private void Start()
    {
        objectives = new();
        map = FindAnyObjectByType<Map>();
        foreach (Vector2 vector2 in cellsObjective)
        {
            int x = ((int)vector2.x);
            int y = ((int)vector2.y);
            objectives.Add(map.MatrixCell[x][y]);
            map.MatrixCell[x][y].MouseEnter();
        }
    }

    private void Update()
    {
        int numberOfEnemiesInObjective = 0;
        int numberOfAlliedsInObjective = 0;
 
        foreach (Cell cell in objectives)
        {
            Squad squad = cell.SquadInCell;
            if(squad != null)
            {
                if (squad.gameObject.GetComponent<AlliedSquad>() != null)
                {
                    numberOfAlliedsInObjective++;
                }
                else if (squad.gameObject.GetComponent<EnemySquad>() != null)
                {
                    numberOfEnemiesInObjective++;
                }
            } 
        }
        Debug.Log("number of enemies in objective" + numberOfEnemiesInObjective);
        Debug.Log("number of allied in objective" + numberOfAlliedsInObjective);
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
