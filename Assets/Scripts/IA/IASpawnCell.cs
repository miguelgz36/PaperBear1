using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASpawnCell : MonoBehaviour
{
    private Squad squadInZone;

    public Squad SquadInZone { get => squadInZone; set => squadInZone = value; }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Squad squadExitting = collision.GetComponentInParent<Squad>();
        if (squadExitting && squadExitting == SquadInZone)
        {
            SquadInZone = null;
        }
    }
}
