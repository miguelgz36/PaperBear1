using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour, Action
{
    private Squad squad = null;

    private bool isBuilding = false;

    private Cell cellToPlaceTrench;

    private GameObject structurePrefab;

    private float timeToBuildInSeconds;

    private Slider buildingSlider;

    private float initialBuildingTime;

    public void Update()
    {
        if (isBuilding)
        {
            float currentTime = Time.unscaledTime - this.initialBuildingTime;
            if (currentTime < timeToBuildInSeconds)
            {
                this.buildingSlider.value = 1 - currentTime / timeToBuildInSeconds;
            }
            else
            {
                this.buildingSlider.value = 0;
                this.BuildStructure();
                isBuilding = false;
            }
        }
        
    }

    public void Execute(Dictionary<CommandParamEnum, object> args)
    {
        this.squad = (Squad)args.GetValueOrDefault(CommandParamEnum.SQUAD);
        this.structurePrefab = (GameObject)args.GetValueOrDefault(CommandParamEnum.STRUCTURE_PREFAB);
        this.cellToPlaceTrench = this.squad.GetComponentInChildren<SquadCellDetector>().CurrentCell;
        this.buildingSlider = (Slider)args.GetValueOrDefault(CommandParamEnum.SLIDER);
        this.timeToBuildInSeconds = this.structurePrefab.GetComponent<Structure>().SecondsToBuild;

        if (cellToPlaceTrench != null && !cellToPlaceTrench.hasStructure())
        {
            this.squad.IsBusy = true;
            this.initialBuildingTime = Time.unscaledTime;
            this.isBuilding = true;
        }
    }

    private void BuildStructure()
    {
        GameObject trench = Instantiate(this.structurePrefab, this.cellToPlaceTrench.gameObject.transform.position, Quaternion.identity);
        cellToPlaceTrench.Structure = trench.GetComponent<Structure>();
        this.squad.IsBusy = false;
    }
}