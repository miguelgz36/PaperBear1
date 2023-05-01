using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private bool isEnemy = false;

    public bool IsEnemy()
    {
        return isEnemy;
    }


}
