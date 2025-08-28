using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IFooter
{
    void Enter();
    void Exit();
    void Init();
}
public class UIManager : MonoBehaviour
{
    [SerializeField]
    Canvas MainCanvas;
    [SerializeField]
    RectTransform ScrollViewPort;
    [SerializeField]
    TextMeshProUGUI GoldText;
    [SerializeField]
    TextMeshProUGUI StageText;

    [SerializeField]
    UIQuest QuestPrefab;
    [SerializeField]
    UISoldier SoldierPrefab;
    [SerializeField]
    public RectTransform QuestContent;
    [SerializeField]
    public RectTransform SoldierContent;
    public UIQuestManager Quest { get; set; }
    public UISoldierManager Soldier { get; set; }

    IFooter _state;
    public IFooter State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;
            _state?.Exit();
            _state = value;
            _state?.Enter();
        }
    }

    private void Start()
    {
        Manager.UI = this;
        var q = new GameObject("QuestManager").AddComponent<UIQuestManager>();
        q.transform.SetParent(gameObject.transform);
        Quest = q;
        q.QuestContent = QuestContent;
        q.UIQuestPrefab = QuestPrefab;

        var s = new GameObject("SoldierManager").AddComponent<UISoldierManager>();
        s.transform.SetParent(gameObject.transform);
        Soldier = s;
        s.SoldierContent = SoldierContent;
        s.SoldierUI = SoldierPrefab;
        Manager.Game.OnGoldChanged += OnGoldChanged;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Manager.Game.Gold++;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Manager.Game.Gold--;
        }
    }
    void OnGoldChanged()
    {
        GoldText.text = "G" + Manager.Game.Gold.ToString();
    }
    public void ToQuest()
    {
        State = Quest;
    }
    public void ToSoldier()
    {
        State = Soldier;
    }
}
