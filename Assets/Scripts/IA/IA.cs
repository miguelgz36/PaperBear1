using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    [SerializeField] List<IASpawnCell> spawnPoints;
    [SerializeField] List<GameObject> squadsToSpawn;
    [SerializeField] float DelayInstructions = 1f;

    private List<IAInstruction> instructions;
    private List<Squad> squadsSpawned;

    public List<IASpawnCell> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
    public List<GameObject> SquadsToSpawn { get => squadsToSpawn; set => squadsToSpawn = value; }
    public List<Squad> SquadsSpawned { get => squadsSpawned; set => squadsSpawned = value; }

    public void Start()
    {
        instructions = new List<IAInstruction>
        {
            new IAInstructionSpawnUnit(this),
            new IAInstructionMoveUnit(this),
        };
        squadsSpawned = new List<Squad>();
        StartCoroutine(StartIA());
    }

    public Squad InstantiateSquad(int unitToSpawnIndex, int spawnPointIndex)
    {
        return Instantiate(SquadsToSpawn[unitToSpawnIndex], SpawnPoints[spawnPointIndex].transform).GetComponent<Squad>();
    }
    private IEnumerator StartIA()
    {
        do
        {
            yield return new WaitForSeconds(DelayInstructions);
            for (int i = 0; i < instructions.Count; i++)
            {
                bool resultInstruction = instructions[i].Execute();
                if (resultInstruction)
                {
                    break;
                }
            }
        }
        while (!LevelStateManager.Instance.IsFinishedGame);
    }


}
