using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrenchButton : MonoBehaviour
{

    [SerializeField] private GameObject trenchButtonObject;

    [SerializeField] private GameObject trench;

    [SerializeField] private float secondsBeforeAppearingForTheFirstTime;

    private DateTime activationTime;

    private bool shouldActivate = false;

    private void Awake()
    {
        activationTime = DateTime.Now.AddSeconds(secondsBeforeAppearingForTheFirstTime);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldActivate && !trenchButtonObject.active && DateTime.Now > activationTime)
        {
            TrenchButtonManager.Instance.SetCurrentSelectedUnit(trenchButtonObject);
            shouldActivate = false;
        }
    }

    private void OnMouseEnter()
    {
        if(!trench.active)
        {
           shouldActivate = true;
        }
    }

    private void OnMouseExit()
    {
        shouldActivate = false;
    }

    public void onClick()
    {
        trench.SetActive(true);
        TrenchButtonManager.Instance.UnselectIfExists(trenchButtonObject);
    }
}
