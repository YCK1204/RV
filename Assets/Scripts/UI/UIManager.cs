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
    UIAlly AllyPrefab;
    [SerializeField]
    public RectTransform QuestContent;
    [SerializeField]
    public RectTransform AllyContent;
    public UIQuestManager Quest { get; set; }
    public UIAllyManager Ally { get; set; }

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

        var s = new GameObject("AllyManager").AddComponent<UIAllyManager>();
        s.transform.SetParent(gameObject.transform);
        Ally = s;
        s.AllyContent = AllyContent;
        s.AllyUI = AllyPrefab;
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
    public void ToAlly()
    {
        State = Ally;
    }
}
