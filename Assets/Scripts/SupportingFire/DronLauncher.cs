using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronLauncher : Placeable
{
    // Start is called before the first frame update

    [SerializeField] GameObject dron;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeployDron(Vector3 target)
    {
        GameObject dronInstance = Instantiate(this.dron, SupportFireManager.Instance.PositionAlliedSupportingFire.position, Quaternion.identity);
        Dron dron = dronInstance.GetComponent<Dron>();
        dron.Deploy(target);
    }
}
