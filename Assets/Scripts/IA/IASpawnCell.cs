using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASpawnCell : MonoBehaviour
{
    public Squad SquadInZone { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SquadCellDetector squadExitting = collision.GetComponent<SquadCellDetector>();
        if(squadExitting) {
            SquadInZone = collision.GetComponentInParent<Squad>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SquadCellDetector squadExitting = collision.GetComponent<SquadCellDetector>();
        if (squadExitting && collision.GetComponentInParent<Squad>() == SquadInZone)
        {
            SquadInZone = null;
        }
    }
}
