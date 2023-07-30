using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private bool isEnemy = false;
    [SerializeField] private FloatingText reloadingText;

    private Squad squad;

    private void Awake()
    {
        squad = GetComponentInParent<Squad>();
    }

    public void StartReloading()
    {
        reloadingText.Show();
    }

    public void StopReloading()
    {
        reloadingText.Hide();
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
