using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInstructionSpawnUnit : IAInstruction
{
    public IAInstructionSpawnUnit(IA ia): base(ia)
    {

    }
    public override bool Execute()
    {
        int triesSpawn = 0;
        int spawnPointIndex = Random.Range(0, ia.SpawnPoints.Count);
        while (triesSpawn < ia.SpawnPoints.Count && ia.SpawnPoints[spawnPointIndex].SquadInZone != null)
        {
            spawnPointIndex++;
            triesSpawn++;
            if (spawnPointIndex >= ia.SpawnPoints.Count)
            {
                spawnPointIndex = 0;
            }
        }

        if (triesSpawn == ia.SpawnPoints.Count) return false;

        triesSpawn = 0;
        int unitToSpawnIndex = Random.Range(0, ia.SquadCooldowns.Count);
        while (triesSpawn < ia.SquadCooldowns.Count && !ia.SquadCooldowns[unitToSpawnIndex].IsValidToSpawn())
        {
            unitToSpawnIndex++;
            triesSpawn++;
            if (unitToSpawnIndex >= ia.SquadCooldowns.Count)
            {
                unitToSpawnIndex = 0;
            }
        }

        if (triesSpawn == ia.SquadCooldowns.Count) return false;

        Squad squadInstance = ia.InstantiateSquad(unitToSpawnIndex, spawnPointIndex);
        ia.SpawnPoints[spawnPointIndex].SquadInZone = squadInstance;
        ia.SquadsSpawned.Add(squadInstance);
        return true;
    }
}
