using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    [SerializeField] List<IASpawnCell> spawnPoints;
    [SerializeField] List<GameObject> squadsToSpawn;
    [SerializeField] float delayInstructions = 1f;

    private List<IAInstruction> instructions;
    private List<PlaceableCooldown> placeableCooldowns;
    private List<Squad> squadsSpawned;

    public List<IASpawnCell> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
    public List<PlaceableCooldown> PlaceableCooldowns { get => placeableCooldowns; set => placeableCooldowns = value; }
    public List<Squad> SquadsSpawned { get => squadsSpawned; set => squadsSpawned = value; }

    public void Start()
    {
        instructions = new List<IAInstruction>
        {
            new IAInstructionSpawnUnit(this),
            new IAInstructionMoveUnit(this),
        };
        placeableCooldowns = CreateCooldowns();
        squadsSpawned = new List<Squad>();
        StartCoroutine(StartIA());
    }

    private List<PlaceableCooldown> CreateCooldowns()
    {
        List<PlaceableCooldown> result = new();

        foreach(GameObject squadToSpawn in squadsToSpawn){
            PlaceableCooldown placeableCooldown = new(squadToSpawn.GetComponent<Placeable>());
            placeableCooldown.Start();
            result.Add(placeableCooldown);
        }

        return result;
    }

    private void Update()
    {
        foreach (PlaceableCooldown placeableCooldown in placeableCooldowns)
        {
            placeableCooldown.Update();
        }
    }

    public Squad InstantiateSquad(int unitToSpawnIndex, int spawnPointIndex)
    {
        GameObject unitSpawned = Instantiate(placeableCooldowns[unitToSpawnIndex].Placeable.gameObject, SpawnPoints[spawnPointIndex].transform);
        Placeable placeable = unitSpawned.GetComponent<Placeable>();
        placeable.PlaceableCooldown = placeableCooldowns[unitToSpawnIndex];
        placeableCooldowns[unitToSpawnIndex].ResetCooldown();
        return unitSpawned.GetComponent<Squad>();
    }
    private IEnumerator StartIA()
    {
        do
        {
            yield return new WaitForSeconds(delayInstructions);
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
