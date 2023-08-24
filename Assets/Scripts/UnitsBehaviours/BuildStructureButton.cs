using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BuildStructureButton : ActionButton
{
    [SerializeField] private GameObject structurePrefab;

    [SerializeField] private SquadCellDetector cellDetector;

    [SerializeField] private Slider buildingSlider;

    public override void execute()
    {
        Dictionary<CommandParamEnum, object> args = new Dictionary<CommandParamEnum, object>();

        args.Add(CommandParamEnum.SQUAD, this.squad);
        args.Add(CommandParamEnum.STRUCTURE_PREFAB, structurePrefab);
        args.Add(CommandParamEnum.SLIDER, this.buildingSlider);

        this.squad.ExecuteAction<Build>(args);

    }
}
