using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour, IFooter
{
    public List<Quest> Quests = new List<Quest>();
    public RectTransform QuestContent;
    private void Start()
    {
        Init();
    }
    public void Add(Quest quest)
    {
        if (Quests.Contains(quest))
            return;
        Quests.Add(quest);
    }
    public void Enter()
    {
        QuestContent.gameObject.SetActive(true);
        foreach (var quest in Quests)
            quest.Openable();
    }
    public void Exit()
    {
        QuestContent.gameObject.SetActive(false);
    }
    public void Init()
    {
        Manager.UI.Quest = this;
        Manager.Game.OnGoldChanged += OnGoldChanged;
    }
    void OnGoldChanged()
    {
        foreach (var quest in Quests)
            quest.Openable();
    }
    private void Update()
    {
        foreach (var quest in Quests)
            quest.UpdateController();
    }
}
