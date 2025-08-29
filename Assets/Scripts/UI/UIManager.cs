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

    public UIQuest QuestPrefab;
    public UIAlly AllyPrefab;
    public UIInventoryItem UIInventoryItemPrefab;
    public UIShopItem UIShopItemPrefab;
    public RectTransform QuestContent;
    public RectTransform AllyContent;
    public RectTransform InventoryContent;
    public RectTransform ShopContent;
    public UIQuestManager Quest { get; set; }
    public UIAllyManager Ally { get; set; }
    public UIInventoryManager Inventory { get; set; }
    public UIShopManager Shop { get; set; }

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
        Init();
    }
    void Init()
    {
        Manager.UI = this;
        InitQuestManager();
        InitAllyManager();
        InitInventoryManager();
        InitShopManager();

        Manager.Game.OnGoldChanged += OnGoldChanged;

        Manager.Game.Gold = Manager.Data.playerData.Gold;
        Manager.Game.StageLevel = Manager.Data.playerData.Stage;
        GoldText.text = "G" + Manager.Game.Gold.ToString();
        StageText.text = "STAGE " + Manager.Game.StageLevel.ToString();
        ToQuest();
    }
    void InitQuestManager()
    {
        var q = new GameObject("QuestManager").AddComponent<UIQuestManager>();
        q.transform.SetParent(gameObject.transform);
        Quest = q;
    }
    void InitAllyManager()
    {
        var s = new GameObject("AllyManager").AddComponent<UIAllyManager>();
        s.transform.SetParent(gameObject.transform);
        Ally = s;
    }
    void InitInventoryManager()
    {
        var i = new GameObject("InventoryManager").AddComponent<UIInventoryManager>();
        i.transform.SetParent(gameObject.transform);
        Inventory = i;
    }
    void InitShopManager()
    {
        var s = new GameObject("ShopManager").AddComponent<UIShopManager>();
        s.transform.SetParent(gameObject.transform);
        Shop = s;
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
    public void ToInventory()
    {
        State = Inventory;
    }
    public void ToShop()
    {
        State = Shop;
    }
}
