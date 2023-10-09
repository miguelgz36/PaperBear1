using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    Squad squad;

    private void Awake()
    {
        squad = GetComponent<Squad>();
    }
    private void OnMouseEnter()
    {
        SelectManager.Instance.SquadReadyToSelect = this;
    }

    private void OnMouseExit()
    {
        SelectManager.Instance.SquadReadyToSelect = null;
    }

    public void SetSelectionUI(bool value)
    {
        squad.SetSelectionUI(value);
    }
}
