using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildStructureButton : ActionButton
{
    [SerializeField] private GameObject trenchPrefab;

    [SerializeField] private SquadCellDetector cellDetector;

    public override void execute()
    {
        Dictionary<CommandParamEnum, object> args = new Dictionary<CommandParamEnum, object>();

        args.Add(CommandParamEnum.SQUAD, squad);
        args.Add(CommandParamEnum.STRUCTURE_PREFAB, trenchPrefab);

        this.squad.ExecuteAction<Build>(args);

    }
}
