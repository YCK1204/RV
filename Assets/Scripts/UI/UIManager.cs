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
    RectTransform QuestContent;
    [SerializeField]
    TextMeshProUGUI GoldText;
    [SerializeField]
    TextMeshProUGUI StageText;

    QuestManager _quest = new QuestManager();
    public QuestManager Quest { get { return _quest; } }

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
        _quest.QuestContent = QuestContent;
        _state = _quest;
        Manager.Game.OnGoldChanged += OnGoldChanged;
        _quest.Init();
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
        State = _quest;
    }
}
