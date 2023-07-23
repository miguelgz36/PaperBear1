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

    private void Start()
    {
        Physics2D.queriesHitTriggers = true;
    }

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

    public void OnClick()
    {
        trench.SetActive(true);
        TrenchButtonManager.Instance.UnselectIfExists(trenchButtonObject);
    }
}
