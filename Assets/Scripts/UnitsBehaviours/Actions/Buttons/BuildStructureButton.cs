using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BuildStructureButton : ActionButton
{
    [SerializeField] private GameObject structurePrefab;

    [SerializeField] private Image imageCooldown;

    public override void execute()
    {
        Dictionary<CommandParamEnum, object> args = new Dictionary<CommandParamEnum, object>();

        args.Add(CommandParamEnum.SQUAD, this.currentSquad);
        args.Add(CommandParamEnum.STRUCTURE_PREFAB, structurePrefab);
        args.Add(CommandParamEnum.IMAGE_COOLDOWN, this.imageCooldown);

        this.currentSquad.ExecuteAction<ActionBuild>(args);

    }
}
