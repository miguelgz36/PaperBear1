using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : MonoBehaviour
{
    [SerializeField] GameObject squadObject;

    private Squad squad = null;

    void Awake()
    {
        this.squad = squadObject.GetComponent<Squad>();
    }

    public void onClick()
    {
        Dictionary<CommandParamEnum, object> args = new Dictionary<CommandParamEnum, object>();

        args.Add(CommandParamEnum.SQUAD, this.squad);
        this.squad.ExecuteAction<MoveToNextCell>(args);
    }
}
