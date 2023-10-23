using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInstructionSpawnSupportFire : IAInstruction
{

    public IAInstructionSpawnSupportFire(IA ia) : base(ia)
    {

    }
    public override bool Execute()
    {
        int supportingFireToSpawnIndex = 0;
        while (supportingFireToSpawnIndex < ia.SupportingFireCooldowns.Count && !ia.SupportingFireCooldowns[supportingFireToSpawnIndex].IsValidToSpawn())
        {
            supportingFireToSpawnIndex++;
        }

        if (supportingFireToSpawnIndex == ia.SupportingFireCooldowns.Count) return false;

        Cell cellWithAlliedSquad = ia.Map.ReturnFirstCellWith<AlliedSquad>();

        if (cellWithAlliedSquad == null) return false;

        ia.PlaceArtillery(supportingFireToSpawnIndex, cellWithAlliedSquad.gameObject.transform.position);

        return true;
    }

}
