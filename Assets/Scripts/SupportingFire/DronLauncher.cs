using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronLauncher : Placeable
{

    [SerializeField] GameObject dron;

    public void DeployDron(Vector3 target, Vector3 origin)
    {
        GameObject dronInstance = Instantiate(this.dron, origin, Quaternion.identity);
        Dron dron = dronInstance.GetComponent<Dron>();
        dron.Deploy(target, origin);
    }
}
