using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuestManager : MonoBehaviour, IFooter
{
    [HideInInspector]
    public UIQuest UIQuestPrefab;
    public List<UIQuest> Quests = new List<UIQuest>();
    public RectTransform QuestContent;
    private void Start()
    {
        Init();
    }
    public void Add(UIQuest quest)
    {
        if (Quests.Contains(quest))
            return;
        Quests.Add(quest);
        Manager.Data.playerData.Quests.Quests.Add(new J_Quest() { Id = quest.Id, Level = quest.Lv });
        Manager.Data.Save();
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
        var quests = Manager.Data.playerData.Quests.Quests;
        foreach (var quest in quests)
        {
            var questUi = Manager.Resource.Instantiate<UIQuest>(UIQuestPrefab, QuestContent);
            var data = Manager.Data.QuestData[quest.Id];
            questUi.Init(data);
            questUi.Lv = quest.Level;
            questUi.OnActivated(data.OpenCost, true);
            Quests.Add(questUi);
        }
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
