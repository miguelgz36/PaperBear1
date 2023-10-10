using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCancel : MonoBehaviour, IAction
{
    private Squad squad = null;


    public bool Execute(Dictionary<CommandParamEnum, object> args)
    {
        squad = (Squad)args.GetValueOrDefault(CommandParamEnum.SQUAD);
        IAction[] actions = squad.GetComponents<IAction>();

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Stop();
        }

        return true;
    }

    public void Stop()
    {
        return;
    }
}
