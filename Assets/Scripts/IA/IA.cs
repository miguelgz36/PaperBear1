using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    [SerializeField] List<IASpawnCell> spawnPoints;
    [SerializeField] List<GameObject> squadsToSpawn;
    private float SpawnUnitDelay = 5f;

    public void Start()
    {
        StartCoroutine(StartIA());
    }


    private void SpawnUnit()
    {
        int triesSpawn = 0;
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        while (triesSpawn < spawnPoints.Count && spawnPoints[spawnPointIndex].SquadInZone != null)
        {
            spawnPointIndex++;
            triesSpawn++;
            if(spawnPointIndex >= spawnPoints.Count)
            {
                spawnPointIndex = 0;
            }
        }

        if (triesSpawn == spawnPoints.Count) return;
         
        int unitToSpawnIndex = Random.Range(0, squadsToSpawn.Count);

        Squad squadInstance = Instantiate(squadsToSpawn[unitToSpawnIndex], spawnPoints[spawnPointIndex].transform).GetComponent<Squad>();
        spawnPoints[spawnPointIndex].SquadInZone = squadInstance;
    }


    private IEnumerator StartIA()
    {
        do
        {
            yield return new WaitForSeconds(SpawnUnitDelay);
            SpawnUnit();
        } 
        while (!LevelStateManager.Instance.IsFinishedGame);
    }


}
