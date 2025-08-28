using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        var questList = Manager.Data.QuestData.Select(q => q.Value).ToList().OrderBy(q => q.Id);
        foreach (var quest in questList)
        {
            var qe = Manager.Instantiate<UIQuest>(UIQuestPrefab, QuestContent);
            qe.Init(quest);
            Quests.Add(qe);
        }

        var quests = Manager.Data.playerData.Quests.Quests;
        foreach (var quest in quests)
        {
            var data = Manager.Data.QuestData[quest.Id];
            UIQuest q = Quests.Find(q => q.Id == quest.Id);
            if (q == null)
                continue;
            q.Lv = quest.Level;
            q.OnActivated(data.OpenCost, true);
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
