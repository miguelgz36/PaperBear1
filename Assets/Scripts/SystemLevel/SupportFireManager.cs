using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportFireManager : Singleton<SupportFireManager>
{
    [SerializeField] GameObject positionAlliedSupportingFire;
    [SerializeField] GameObject positionEnemySupportingFire;

    public GameObject PositionAlliedSupportingFire { get => positionAlliedSupportingFire; }
    public GameObject PositionEnemySupportingFire { get => positionEnemySupportingFire; }
}
