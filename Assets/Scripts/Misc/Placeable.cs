using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private float basicCost;

    public float BasicCost { get => basicCost; }
}
