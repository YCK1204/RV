using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour, IFooter
{
    List<Soldier> _soldiers = new List<Soldier>();
    public RectTransform SoldierContent;
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
    }
    public void Add(Soldier soldier)
    {
        if (!_soldiers.Contains(soldier))
            _soldiers.Add(soldier);
    }
    void OnGoldChanged()
    {
        foreach (var soldier in _soldiers)
            soldier.Openable();
    }
}
