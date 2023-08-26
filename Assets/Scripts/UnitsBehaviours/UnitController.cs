using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitController : MonoBehaviour
{
    [SerializeField] private bool isEnemy = false;
    [SerializeField] private Slider sliderAmmo;
    [SerializeField] private Aim aim;

    public Slider SliderAmmo { get => sliderAmmo; set => sliderAmmo = value; }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void AimTarget(Collider2D collider2D)
    {
        aim.AimTarget(collider2D);
    }

    public void UnAimTarget(Collider2D collider2D)
    {
        aim.UnAimTarget(collider2D);
    }
}
