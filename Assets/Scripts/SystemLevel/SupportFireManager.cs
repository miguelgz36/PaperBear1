using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportFireManager : Singleton<SupportFireManager>
{
    [SerializeField] Transform positionAlliedSupportingFire;
    [SerializeField] Transform positionEnemySupportingFire;

    public Transform PositionAlliedSupportingFire { get => positionAlliedSupportingFire; }
    public Transform PositionEnemySupportingFire { get => positionEnemySupportingFire; }
}
