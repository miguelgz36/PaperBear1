
using UnityEngine;
using UnityEngine.UI;

public abstract class ActionButton : MonoBehaviour
{

    private Button button;
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
                button.interactable = !currentSquad.IsBusy;
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

    public void OnClick()
    {
        if (currentSquad != null && !this.currentSquad.IsBusy)
        {
            this.execute();
        }
    }

    public abstract void execute();
}
