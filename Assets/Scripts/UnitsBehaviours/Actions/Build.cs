using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour, Action
{
    private Squad squad = null;

    public void Execute(Dictionary<CommandParamEnum, object> args)
    {
        this.squad = (Squad)args.GetValueOrDefault(CommandParamEnum.SQUAD);
        GameObject structurePrefab = (GameObject)args.GetValueOrDefault(CommandParamEnum.STRUCTURE_PREFAB);

        Cell cellToPlaceTrench = this.squad.GetComponentInChildren<SquadCellDetector>().CurrentCell;

        if (cellToPlaceTrench != null && !cellToPlaceTrench.hasStructure())
        {
            GameObject trench = Instantiate(structurePrefab, cellToPlaceTrench.gameObject.transform.position, Quaternion.identity);
            cellToPlaceTrench.Structure = trench.GetComponent<Structure>();
        }
    }
}
