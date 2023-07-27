using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private int capPopulation;
    private PlacementPlaceable sourceSpawner;
    public PlacementPlaceable OriginButton { set => sourceSpawner = value; }

    public float CoolDown { get => cooldown; }
    public int MaxCapPopulation { get => capPopulation; }

    public void ReducePopulation()
    {
        if (sourceSpawner)
        {
            sourceSpawner.DecreasePopulation();
        }
    }
}
