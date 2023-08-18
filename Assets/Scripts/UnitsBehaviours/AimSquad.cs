using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSquad : MonoBehaviour
{
    [SerializeField] Squad squad;

    private void OnTriggerStay2D(Collider2D collision)
    {
        squad.AimTarget(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        squad.UnAimTarget(collision);
    }
}
