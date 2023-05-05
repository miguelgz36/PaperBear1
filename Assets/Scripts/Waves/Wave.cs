using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class Wave : ScriptableObject
{
    [SerializeField] private List<AttackGroup> attackGroups;
    [SerializeField] private float timeBetweenAssaultGroups = 60.0f;


    public List<AttackGroup> AttackGroups { get => attackGroups; }
    public float TimeBetweenAssaultGroups { get => timeBetweenAssaultGroups; }
}
