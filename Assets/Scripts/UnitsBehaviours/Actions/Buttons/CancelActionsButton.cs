using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CancelActionsButton : ActionButton
{


    public override void execute()
    {

        Dictionary<CommandParamEnum, object> args = new()
        {
            { CommandParamEnum.SQUAD, this.currentSquad },
        };

        this.currentSquad.ExecuteAction<ActionCancel>(args);
    }

    protected override bool ButtonIsAvailableToClick()
    {
        return true;
    }

    protected override bool SquadIsAvailableToExecuteOrders()
    {
        return currentSquad != null;
    }
}
