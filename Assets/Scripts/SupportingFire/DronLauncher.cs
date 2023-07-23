using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronLauncher : Placeable
{

    [SerializeField] GameObject dron;

    public void DeployDron(Vector3 target)
    {
        GameObject dronInstance = Instantiate(this.dron, SupportFireManager.Instance.PositionAlliedSupportingFire.transform.position, Quaternion.identity);
        Dron dron = dronInstance.GetComponent<Dron>();
        dron.Deploy(target);
    }
}
