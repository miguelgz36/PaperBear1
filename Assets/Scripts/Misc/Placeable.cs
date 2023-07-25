using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private float cooldown;

    public float CoolDown { get => cooldown; }
}
