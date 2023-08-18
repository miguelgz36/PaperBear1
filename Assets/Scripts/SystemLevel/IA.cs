using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<GameObject> squadsToSpawn;
    private float SpawnUnitDelay = 10f;

    public void Start()
    {
        StartCoroutine(StartIA());
    }


    private void SpawnUnit()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        int unitToSpawnIndex = Random.Range(0, squadsToSpawn.Count);

        Instantiate(squadsToSpawn[unitToSpawnIndex], spawnPoints[spawnPointIndex]);
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
