using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitController : MonoBehaviour
{
    [SerializeField] private bool isEnemy = false;
    [SerializeField] private Slider sliderAmmo;

    private Squad squad;

    public Slider SliderAmmo { get => sliderAmmo; set => sliderAmmo = value; }

    private void Awake()
    {
        squad = GetComponentInParent<Squad>();
    }

   
    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void OnDestroy()
    {
        if (squad)
        {
            squad.RemoveUnit(gameObject);
        }
    }


}
