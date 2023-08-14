using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildStructureButton : MonoBehaviour
{
    [SerializeField] private GameObject trenchPrefab;

    [SerializeField] private Squad squad;

    [SerializeField] private SquadCellDetector cellDetector;

    public void onClick()
    {
        Dictionary<CommandParamEnum, object> args = new Dictionary<CommandParamEnum, object>();

        args.Add(CommandParamEnum.SQUAD, squad);
        args.Add(CommandParamEnum.STRUCTURE_PREFAB, trenchPrefab);

        this.squad.ExecuteAction<Build>(args);
    }
}
