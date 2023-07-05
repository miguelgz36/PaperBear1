using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrenchButton : MonoBehaviour
{

    [SerializeField] private GameObject trenchButtonObject;

    [SerializeField] private GameObject trench;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if(!trench.active)
        {
            TrenchButtonManager.Instance.SetCurrentSelectedUnit(trenchButtonObject);
        }
    }

    public void onClick()
    {
        trench.SetActive(true);
        TrenchButtonManager.Instance.UnselectIfExists(trenchButtonObject);
    }
}
