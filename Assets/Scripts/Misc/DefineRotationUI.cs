using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineRotationUI : MonoBehaviour
{

    void Update()
    {
        if (transform.rotation.eulerAngles.z >= -90 && transform.rotation.eulerAngles.z < 90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } 
        else if(transform.rotation.eulerAngles.z >= 90 && transform.rotation.eulerAngles.z <= 270)
        {
            transform.localScale = new Vector3(-1, -1, 1);
        }
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }
}
