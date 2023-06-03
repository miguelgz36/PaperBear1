using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWaves : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;
    [SerializeField] private List<Transform> spawnPoints;

    bool finalWave = false;

    private void Start()
    {
        StartCoroutine(WaveLevel());
    }

    void Update()
    {
        if (finalWave && FindObjectsOfType<EnemyMove>().Length == 0)
        {
            LevelStateManager.Instance.Win();
        }
    }

    IEnumerator WaveLevel()
    {
        foreach(Wave wave in waves){
            List<AttackGroup> attackGroups = wave.AttackGroups;
            foreach(AttackGroup attackGroup in attackGroups)
            {
                List<GameObject> enemySquads = attackGroup.Squads;
                foreach(GameObject squad in enemySquads)
                {
                   int randomNumber = Random.Range(0, spawnPoints.Count);
                   Instantiate(squad, spawnPoints[randomNumber]);
                   yield return new WaitForSeconds(attackGroup.TimeToSpawn);
                }
                yield return new WaitForSeconds(wave.TimeBetweenAssaultGroups);
            }
        }
        finalWave = true;
    }
}
