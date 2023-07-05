using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrenchButtonManager : Singleton<TrenchButtonManager>
{

    private GameObject currentActiveButton;

    protected override void Awake()
    {
        base.Awake();
        currentActiveButton = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentSelectedUnit(GameObject buttonToActivate)
    {
        if (currentActiveButton != null)
        {
            this.currentActiveButton.SetActive(false);
        }
        buttonToActivate.SetActive(true);
       currentActiveButton = buttonToActivate;
    }

    public void UnselectIfExists(GameObject buttonToUnselect)
    {
        if (currentActiveButton == buttonToUnselect)
        {
            currentActiveButton.SetActive(false);
            currentActiveButton = null;
        }
    }
}
