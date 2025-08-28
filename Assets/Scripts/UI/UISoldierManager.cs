using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoldierManager : MonoBehaviour, IFooter
{
    [HideInInspector]
    public UISoldier SoldierUI;

    List<UISoldier> _soldiers = new List<UISoldier>();
    public RectTransform SoldierContent;
    int _maxId = 0;
    private void Start()
    {
        Init();
    }
    public void Enter()
    {
        SoldierContent.gameObject.SetActive(true);
    }
    public void Exit()
    {
        SoldierContent.gameObject.SetActive(false);
    }
    public void Init()
    {
        Manager.UI.Soldier = this;
        Manager.Game.OnGoldChanged += OnGoldChanged;
        var soldiers = Manager.Data.playerData.Soldiers.Soldiers;
        foreach (var soldier in soldiers)
        {
            _maxId = Math.Max(_maxId, soldier.Id);
            var soldierUi = Manager.Resource.Instantiate<UISoldier>(SoldierUI, SoldierContent);
            var data = Manager.Data.SoldierData[soldier.CharId];
            soldierUi.Init(data);
            soldierUi.Id = soldier.Id;
            soldierUi.Lv = soldier.Level;
            soldierUi.Upgrade = soldier.Upgrade;
            soldierUi.OnClickOpenOrLvUp(true);
            _soldiers.Add(soldierUi);
        }
    }
    public void Add(UISoldier soldier)
    {
        if (!_soldiers.Contains(soldier))
        {
            _soldiers.Add(soldier);
            soldier.Id = ++_maxId;
            J_Soldier js = new J_Soldier()
            {
                Id = soldier.Id,
                CharId = soldier.CharId,
                Level = soldier.Lv,
                Upgrade = soldier.Upgrade
            };
            Manager.Data.playerData.Soldiers.Soldiers.Add(js);
            Manager.Data.Save();
        }
    }
    void OnGoldChanged()
    {
        foreach (var soldier in _soldiers)
            soldier.Openable();
    }
}
