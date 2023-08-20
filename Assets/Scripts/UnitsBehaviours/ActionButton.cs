using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ActionButton : MonoBehaviour
{
    [SerializeField] protected Squad squad;
    [SerializeField] private Button button;


    private void Update()
    {
        if (this.squad.IsBusy) 
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void onClick()
    {
        if (!this.squad.IsBusy)
        {
            this.execute();
        }
    }

    public abstract void execute();
}
