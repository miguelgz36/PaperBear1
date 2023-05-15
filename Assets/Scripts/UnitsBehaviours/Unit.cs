using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private bool isEnemy = false;

    private Squad squad;


    private void Awake()
    {
        squad = GetComponentInParent<Squad>();
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void OnDestroy()
    {
        if (squad)
        {
            squad.RemoveUnit(gameObject);
        }
    }


}
