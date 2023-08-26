using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private int capPopulation;
    private PlaceableCooldown placeableCooldown;
    public PlaceableCooldown PlaceableCooldown { set => placeableCooldown = value; get => placeableCooldown; }

    public float CoolDown { get => cooldown; }
    public int MaxCapPopulation { get => capPopulation; }

    public void ReducePopulation()
    {
        if (capPopulation != 0)
        {
            placeableCooldown.DecreasePopulation();
        }
    }
}
