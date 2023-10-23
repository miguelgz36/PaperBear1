using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{

    [SerializeField] float coverage = 0.5f;
    [SerializeField] private float secondsToBuild = 0f;

    public float SecondsToBuild { get => secondsToBuild; set => secondsToBuild = value; }

    public float ReduceDamage(float damage)
    {
        return damage - (damage * coverage);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        ChangeStructureUnit(collision, this);
    }

    private void ChangeStructureUnit(Collider2D collision, Structure structure)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.OnStructure = structure;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ChangeStructureUnit(collision, null);

    }
}
