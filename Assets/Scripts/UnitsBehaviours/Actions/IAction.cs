using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    bool Execute(Dictionary<CommandParamEnum, object> args);

    void Stop();
}
