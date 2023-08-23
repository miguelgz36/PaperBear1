using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    [SerializeField] List<IASpawnCell> spawnPoints;
    [SerializeField] List<Placeable> squadsToSpawn;
    [SerializeField] List<Placeable> supportingFireToSpawn;
    [SerializeField] float delayInstructions = 1f;

    private List<IAInstruction> instructions;
    private List<PlaceableCooldown> squadCooldowns;
    private List<PlaceableCooldown> supportingFireCooldowns;
    private List<Squad> squadsSpawned;
    private Map map;

    public List<IASpawnCell> SpawnPoints { get => spawnPoints; }
    public List<PlaceableCooldown> SquadCooldowns { get => squadCooldowns; }
    public List<PlaceableCooldown> SupportingFireCooldowns { get => supportingFireCooldowns; }
    public List<Squad> SquadsSpawned { get => squadsSpawned; }
    public Map Map { get => map; }

    private void Awake()
    {
        map = FindAnyObjectByType<Map>();
    }

    public void Start()
    {
        instructions = new List<IAInstruction>
        {
            new IAInstructionSpawnUnit(this),
            new IAInstructionSpawnSupportFire(this),
            new IAInstructionMoveUnit(this),
        };
        squadCooldowns = CreateCooldowns(squadsToSpawn);
        supportingFireCooldowns = CreateCooldowns(supportingFireToSpawn);
        squadsSpawned = new List<Squad>();
        StartCoroutine(StartIA());
    }

    private List<PlaceableCooldown> CreateCooldowns(List<Placeable> listToSpawn)
    {
        List<PlaceableCooldown> result = new();

        foreach(Placeable placeableToSpawn in listToSpawn){
            PlaceableCooldown placeableCooldown = new(placeableToSpawn);
            placeableCooldown.Start();
            result.Add(placeableCooldown);
        }

        return result;
    }


    private void Update()
    {
        foreach (PlaceableCooldown placeableCooldown in squadCooldowns)
        {
            placeableCooldown.Update();
        }
        foreach (PlaceableCooldown placeableCooldown in supportingFireCooldowns)
        {
            placeableCooldown.Update();
        }
    }

    public Squad InstantiateSquad(int unitToSpawnIndex, int spawnPointIndex)
    {
        GameObject unitSpawned = Instantiate(squadCooldowns[unitToSpawnIndex].Placeable.gameObject, SpawnPoints[spawnPointIndex].transform);
        Placeable placeable = unitSpawned.GetComponent<Placeable>();
        placeable.PlaceableCooldown = squadCooldowns[unitToSpawnIndex];
        squadCooldowns[unitToSpawnIndex].ResetCooldown();
        return unitSpawned.GetComponent<Squad>();
    }

    public void PlaceArtillery(int supportingFireToPlaceIndex, Vector3 positionToPlace)
    {
        GameObject artilleryInsantiate = Instantiate(supportingFireCooldowns[supportingFireToPlaceIndex].Placeable.gameObject, SupportFireManager.Instance.PositionEnemySupportingFire.transform.position, Quaternion.identity);
        Artillery artillery = artilleryInsantiate.GetComponent<Artillery>();
        if (artillery)
        {
            artillery.FireShells(positionToPlace, SupportFireManager.Instance.PositionEnemySupportingFire.transform.position);
        }
        DronLauncher dronLauncher = artilleryInsantiate.GetComponent<DronLauncher>();
        if(dronLauncher)
        {
            dronLauncher.DeployDron(positionToPlace, SupportFireManager.Instance.PositionEnemySupportingFire.transform.position);
        }
        supportingFireCooldowns[supportingFireToPlaceIndex].ResetCooldown();
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
