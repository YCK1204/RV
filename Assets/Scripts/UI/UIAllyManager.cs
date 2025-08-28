using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAllyManager : MonoBehaviour, IFooter
{
    [HideInInspector]
    public UIAlly AllyUI;

    List<UIAlly> _soldiers = new List<UIAlly>();
    public RectTransform AllyContent;
    int _maxId = 0;
    private void Start()
    {
        Init();
    }
    public void Enter()
    {
        AllyContent.gameObject.SetActive(true);
    }
    public void Exit()
    {
        AllyContent.gameObject.SetActive(false);
    }
    public void Init()
    {
        Manager.UI.Ally = this;
        Manager.Game.OnGoldChanged += OnGoldChanged;
        var soldiers = Manager.Data.playerData.Allys.Allys;
        foreach (var soldier in soldiers)
        {
            _maxId = Math.Max(_maxId, soldier.Id);
            var soldierUi = Manager.Resource.Instantiate<UIAlly>(AllyUI, AllyContent);
            var data = Manager.Data.AllyData[soldier.CharId];
            soldierUi.Init(data);
            soldierUi.Id = soldier.Id;
            soldierUi.Lv = soldier.Level;
            soldierUi.Upgrade = soldier.Upgrade;
            _soldiers.Add(soldierUi);
        }
    }
    public void Add(UIAlly soldier)
    {
        if (!_soldiers.Contains(soldier))
        {
            _soldiers.Add(soldier);
            soldier.Id = ++_maxId;
            J_Ally js = new J_Ally()
            {
                Id = soldier.Id,
                CharId = soldier.CharId,
                Level = soldier.Lv,
                Upgrade = soldier.Upgrade
            };
            Manager.Data.playerData.Allys.Allys.Add(js);
            Manager.Data.Save();
        }
    }
    void OnGoldChanged()
    {
        foreach (var soldier in _soldiers)
            soldier.Openable();
    }
}
