using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : ActionButton
{
    public override void execute()
    {
        Dictionary<CommandParamEnum, object> args = new Dictionary<CommandParamEnum, object>();

        args.Add(CommandParamEnum.SQUAD, this.squad);
        this.squad.ExecuteAction<MoveToNextCell>(args);
    }
}
