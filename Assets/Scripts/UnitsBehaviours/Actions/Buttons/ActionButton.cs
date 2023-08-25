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
        button.interactable = !this.squad.IsBusy;
    }

    public void OnClick()
    {
        if (!this.squad.IsBusy)
        {
            this.execute();
        }
    }

    public abstract void execute();
}
