using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIQuest : MonoBehaviour
{
    [SerializeField]
    Image QuestIcon;
    [SerializeField]
    TextMeshProUGUI QuestUpgradeText;

    [SerializeField]
    Image QuestCooldownImg;
    [SerializeField]
    TextMeshProUGUI QuestCooldownText;
    [SerializeField]
    TextMeshProUGUI QuestGoldText;
    [SerializeField]
    TextMeshProUGUI QuestNameText;

    [SerializeField]
    Button OpenOrUpgradeBtn;
    [SerializeField]
    TextMeshProUGUI ExtraGoldText;
    [SerializeField]
    TextMeshProUGUI UpgradeCostText;
    [SerializeField]
    TextMeshProUGUI OpenCostText;

    [HideInInspector]
    public bool IsActive = false;
    [SerializeField]
    QuestData Data;


    public int Id
    {
        get { return Data.Id; }
    }
    public long _lv = 0;
    public long Lv
    {
        get { return _lv; }
        set
        {
            if (_lv == value)
                return;
            _lv = value;
            QuestUpgradeText.text = "+" + _lv.ToString();
            QuestGold = Data.BaseGoldReward + (Data.ExtraGoldPerUpgrade * _lv);
            OnLevelChanged();
        }
    }
    long _questGold = 0;
    long QuestGold
    {
        get { return _questGold; }
        set
        {
            _questGold = value;
            QuestGoldText.text = "G" + _questGold.ToString();
        }
    }
    public void Init(QuestData data)
    {
        Data = data;
        QuestIcon.sprite = Data.Icon;
        QuestNameText.text = Data.QuestName;
        OpenCostText.text = "G" + Data.OpenCost.ToString();
        QuestGold = Data.BaseGoldReward;
        long m = (long)(Data.BaseCooldown / 60);
        long s = (long)(Data.BaseCooldown % 60);
        QuestCooldownText.text = $"{m:D2}:{s:D2}";
        ExtraGoldText.text = "+" + Data.ExtraGoldPerUpgrade.ToString();
        UpgradeCostText.text = "G " + Data.UpgradeCost.ToString();
    }
    public bool Openable()
    {
        bool openable = false;
        if (IsActive)
            openable = Manager.Game.Gold >= Data.UpgradeCost;
        else
            openable = Manager.Game.Gold >= Data.OpenCost;

        OpenOrUpgradeBtn.interactable = openable;
        return openable;
    }
    public void OnClick()
    {
        if (IsActive == false)
        {
            if (Manager.Game.Gold < Data.OpenCost)
                return;
            IsActive = true;
            OnActivated(Data.OpenCost);
        }
        else
        {
            if (Manager.Game.Gold < Data.UpgradeCost)
                return;
            Manager.Game.Gold -= Data.UpgradeCost;
            Lv++;
            QuestGold = Data.BaseGoldReward + (Data.ExtraGoldPerUpgrade * Lv);
        }
    }
    public void OnActivated(long openCost, bool loaded = false)
    {
        if (!loaded)
        {
            Manager.Game.Gold -= openCost;
            J_Quest quest = new J_Quest() { Id = Id, Level = Lv };
            var quests = Manager.Data.playerData.Quests.Quests;
            quests.Add(quest);
            Manager.Data.Save();
        }
        IsActive = true;
        OpenCostText.gameObject.SetActive(false);
        UpgradeCostText.gameObject.SetActive(true);
        ExtraGoldText.gameObject.SetActive(true);
    }
    float elapsed = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Manager.UI.Quest.Add(this);
        }
    }
    public void UpdateController()
    {
        if (!IsActive)
            return;
        elapsed += Time.deltaTime;
        var scale = QuestCooldownImg.rectTransform.localScale;
        scale.x = Mathf.Clamp01(1f - (elapsed / Data.BaseCooldown));
        QuestCooldownImg.rectTransform.localScale = scale;
        int timer = ((int)(Data.BaseCooldown - elapsed));
        int m = timer / 60;
        int s = timer % 60;
        QuestCooldownText.text = $"{m:D2}:{s:D2}";
        if (elapsed >= Data.BaseCooldown)
        {
            Manager.Game.Gold += QuestGold;
            elapsed = 0;
        }
    }
    void OnLevelChanged()
    {
        var quest = Manager.Data.playerData.Quests.Quests.Where(q => q.Id == Id).FirstOrDefault();

        if (quest == null) return;
        quest.Level = Lv;
        Manager.Data.Save();
    }
}
