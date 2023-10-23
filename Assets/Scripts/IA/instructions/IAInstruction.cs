using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAInstruction
{
    protected IA ia;

    public IAInstruction(IA ia)
    {
        this.ia = ia;
    }
    public abstract bool Execute();
}
