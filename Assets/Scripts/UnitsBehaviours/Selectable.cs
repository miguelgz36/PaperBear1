using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] GameObject selectionUI;
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
        selectionUI.SetActive(value);
    }
}