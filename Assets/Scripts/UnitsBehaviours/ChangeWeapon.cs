using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : Aim
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        LockTarget(collision);

        Debug.Log(collision.gameObject.ToString());
        Debug.Log("tag: " + collision.gameObject.tag);
        if (collision.gameObject.tag.Contains("Tank"))
        {
            Debug.Log("Change into bazooka");
        } else
        {
            if(collision.gameObject.tag.Contains("Enemy"))
            {
                Debug.Log("Change into rifle");
            }
        }
    }
}
