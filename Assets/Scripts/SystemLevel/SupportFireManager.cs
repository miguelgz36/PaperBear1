using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportFireManager : Singleton<SupportFireManager>
{
    [SerializeField] GameObject positionAlliedSupportingFire;
    [SerializeField] Transform positionEnemySupportingFire;

    public GameObject PositionAlliedSupportingFire { get => positionAlliedSupportingFire; }
    public Transform PositionEnemySupportingFire { get => positionEnemySupportingFire; }
}
