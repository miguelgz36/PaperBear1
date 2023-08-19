using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    [SerializeField] List<IASpawnCell> spawnPoints;
    [SerializeField] List<GameObject> squadsToSpawn;
    [SerializeField] float DelayInstructions = 5f;

    private List<IAInstruction> instructions;

    public List<IASpawnCell> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
    public List<GameObject> SquadsToSpawn { get => squadsToSpawn; set => squadsToSpawn = value; }

    public void Start()
    {
        instructions = new List<IAInstruction>
        {
            new IAInstructionSpawnUnit(this)
        };
        StartCoroutine(StartIA());
    }


    private void SpawnUnit()
    {
        int triesSpawn = 0;
        int spawnPointIndex = Random.Range(0, SpawnPoints.Count);
        while (triesSpawn < SpawnPoints.Count && SpawnPoints[spawnPointIndex].SquadInZone != null)
        {
            spawnPointIndex++;
            triesSpawn++;
            if(spawnPointIndex >= SpawnPoints.Count)
            {
                spawnPointIndex = 0;
            }
        }

        if (triesSpawn == SpawnPoints.Count) return;
         
        int unitToSpawnIndex = Random.Range(0, SquadsToSpawn.Count);

        Squad squadInstance = Instantiate(SquadsToSpawn[unitToSpawnIndex], SpawnPoints[spawnPointIndex].transform).GetComponent<Squad>();
        SpawnPoints[spawnPointIndex].SquadInZone = squadInstance;
    }

    public Squad InstantiateSquad(int unitToSpawnIndex, int spawnPointIndex)
    {
        return Instantiate(SquadsToSpawn[unitToSpawnIndex], SpawnPoints[spawnPointIndex].transform).GetComponent<Squad>();
    }
    private IEnumerator StartIA()
    {
        int indexInstruction = 0;
        do
        {
            yield return new WaitForSeconds(DelayInstructions);
            bool resultInstruction = instructions[indexInstruction].Execute();
            if (resultInstruction)
            {
                indexInstruction = 0;
            } 
            else
            {
                indexInstruction++;
            }
            if (indexInstruction == instructions.Count)
            {
                indexInstruction = 0;
            }
        } 
        while (!LevelStateManager.Instance.IsFinishedGame);
    }


}
