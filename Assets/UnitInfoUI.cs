using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitInfoUI : MonoBehaviour
{
    [SerializeField] private Image unitIconImage;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider ammoSlider;


    private UnitController unitController;

    public UnitController UnitController { get => unitController; set => unitController = value; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(unitController == null)
        {
            this.HideUI();
        }
        else
        {
            Health health = this.unitController.GetComponentInChildren<Health>();
            FireWeapon fireWeapon = this.unitController.GetComponentsInChildren<FireWeapon>()[0];

            healthSlider.value = health.CurrentHealth/health.BaseHealth;
            ammoSlider.value = fireWeapon.CurrentAmmo / fireWeapon.AmmoPerMagazine;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        this.unitIconImage.sprite = sprite;
    }

    public void ShowUI()
    {
        this.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }
}
