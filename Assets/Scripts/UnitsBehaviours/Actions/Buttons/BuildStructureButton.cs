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

        Dictionary<CommandParamEnum, object> args = new()
        {
            { CommandParamEnum.SQUAD, this.currentSquad },
            { CommandParamEnum.STRUCTURE_PREFAB, structurePrefab }
        };

        this.currentSquad.ExecuteAction<ActionBuild>(args);
    }

    protected override void UpdateCoolDown()
    {
        base.UpdateCoolDown();
        ActionBuild actionBuild = this.currentSquad.GetComponent<ActionBuild>();
        imageCooldown.fillAmount = actionBuild.FillAmount;
    }

}
