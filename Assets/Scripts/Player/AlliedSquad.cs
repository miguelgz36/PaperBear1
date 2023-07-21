using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedSquad : Placeable
{

    private void OnMouseEnter()
    {
        SelectManager.Instance.SquadReadyToSelect = this;
    }

    private void OnMouseExit()
    {
        Debug.LogError("EXUT");
        SelectManager.Instance.SquadReadyToSelect = null;
    }
}
