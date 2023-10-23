using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SquadUIManager : Singleton<SquadUIManager>
{

    [SerializeField] private SquadBanner squadBanner;
    [SerializeField] private GameObject squadInfo;

    private UnitInfoUI[] unitInfos;

    // Start is called before the first frame update
    void Start()
    {
        this.unitInfos = squadInfo.GetComponentsInChildren<UnitInfoUI>();
        this.HideUnitsInfo();
        this.HideSquadBanner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSquadUI(Selectable selectable)
    {
        Squad squad = selectable.Squad;
        if (squad.gameObject.GetComponent<AlliedSquad>() != null)
        {
            this.ShowSquadBanner(squad);
            this.HideUnitsInfo();
            this.ShowUnitsInfo(squad.Units);
        }
    }

    public void ShowUnitsInfo(List<UnitController> units)
    {
        for (int index = 0; index < units.Count && index < this.unitInfos.Length; index++)
        {
            this.unitInfos[index].UnitController = units[index];
            this.unitInfos[index].SetSprite(units[index].Icon);
            this.ShowUnitInfo(this.unitInfos[index]);
        }
    }

    public void ShowUnitInfo(UnitInfoUI unitInfoUI)
    {
        unitInfoUI.ShowUI();
    }

    public void HideUnitsInfo()
    {
        for (int index = 0; index < this.unitInfos.Length; index++)
        {
            this.HideUnitInfo(this.unitInfos[index]);
        }
    }

    public void HideUnitInfo(UnitInfoUI unitInfoUI)
    {
        unitInfoUI.HideUI();
    }

    public void ShowSquadBanner(Squad squad)
    {
        this.squadBanner.Squad = squad;
        this.squadBanner.SetSprite(squad.Icon);
        this.ShowSquadBanner();
    }

    public void ShowSquadBanner()
    {
        this.squadBanner.ShowUI();
    }

    public void HideSquadBanner()
    {
        this.squadBanner.HideUI();
    }
}
