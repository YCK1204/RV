using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : IFooter
{
    public List<Quest> Quests = new List<Quest>();
    public RectTransform QuestContent;
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
        {
            quest.Openable();
            if (!quest.IsActive)
                continue;
            quest.LoopBuff();
        }
    }
    public void Exit()
    {
        QuestContent.gameObject.SetActive(false);
    }
    public void Init()
    {
        Manager.Game.OnGoldChanged += OnGoldChanged;
    }
    void OnGoldChanged()
    {
        foreach (var quest in Quests)
            quest.Openable();
    }
}
