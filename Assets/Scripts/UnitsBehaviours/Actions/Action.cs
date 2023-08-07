using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Action
{
   void Execute(Dictionary<CommandParamEnum, object> args);
}
