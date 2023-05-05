using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Attack group Config", fileName = "New Attack group Config")]
public class AttackGroup : ScriptableObject
{

    [SerializeField] private List<GameObject> squads;
    [SerializeField] private float timeToSpawn = 1.0f;

    public float TimeToSpawn { get => timeToSpawn; }
    public List<GameObject> Squads { get => squads; }
}
