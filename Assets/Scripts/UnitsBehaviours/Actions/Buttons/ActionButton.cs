
using UnityEngine;
using UnityEngine.UI;

public abstract class ActionButton : MonoBehaviour
{

    protected Button button;
    protected Squad currentSquad;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if(SelectManager.Instance.ObjectSelected != null)
        {
            AlliedSquad alliedSquad = SelectManager.Instance.ObjectSelected.GetComponent<AlliedSquad>();
            if (alliedSquad)
            {
                currentSquad = SelectManager.Instance.ObjectSelected.GetComponent<Squad>();
                button.interactable = ButtonIsAvailableToClick();
            }
            else
            {
                button.interactable = false;
                currentSquad = null;
            }
        } 
        else
        {
            button.interactable = false;
            currentSquad = null;
        }
    }

    protected virtual bool ButtonIsAvailableToClick()
    {
        return !currentSquad.IsBusy;
    }

    protected virtual bool SquadIsAvailableToExecuteOrders()
    {
        return currentSquad != null && !this.currentSquad.IsBusy;
    }

    public void OnClick()
    {
        if (SquadIsAvailableToExecuteOrders())
        {
            this.execute();
        }
    }

    public abstract void execute();
}
