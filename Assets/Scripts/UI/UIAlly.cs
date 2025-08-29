using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAlly : MonoBehaviour
{
    [SerializeField]
    Image AllyIcon;
    [SerializeField]
    TextMeshProUGUI UpgradeText;
    [SerializeField]
    TextMeshProUGUI LevelText;

    [SerializeField]
    TextMeshProUGUI AllyName;
    [SerializeField]
    TextMeshProUGUI AttackPowerText;
    [SerializeField]
    TextMeshProUGUI HealthText;
    [SerializeField]
    TextMeshProUGUI DefensePowerText;

    [SerializeField]
    Button UpgradeBtn;
    [SerializeField]
    TextMeshProUGUI UpgradeCostText;

    [SerializeField]
    Button LvUpBtn;
    [SerializeField]
    TextMeshProUGUI LvUpCostText;

    #region data
    AllyData _data;

    public int Id { get; set; }
    public int CharId { get; set; }

    long _lv = 0;
    public long Lv
    {
        get { return _lv; }
        set
        {
            if (_lv == value)
                return;
            _lv = value;
            LevelText.text = "Lv." + _lv.ToString();
            AttackPower = _data.Attack + (_lv * _data.LvUpAttack) + (Upgrade * _data.BaseUpgradeAttack);
            Health = _data.Health + (_lv * _data.LvUpHealth) + (Upgrade * _data.BaseUpgradeHealth);
            DefensePower = _data.Defense + (_lv * _data.LvUpDefense) + (Upgrade * _data.BaseUpgradeDefense);
        }
    }
    long _upgrade = 0;
    public long Upgrade
    {
        get { return _upgrade; }
        set
        {
            if (_upgrade == value)
                return;
            _upgrade = value;
            UpgradeText.text = "+" + _upgrade.ToString();
            AttackPower = _data.Attack + (_lv * _data.LvUpAttack) + (Upgrade * _data.BaseUpgradeAttack);
            Health = _data.Health + (_lv * _data.LvUpHealth) + (Upgrade * _data.BaseUpgradeHealth);
            DefensePower = _data.Defense + (_lv * _data.LvUpDefense) + (Upgrade * _data.BaseUpgradeDefense);
            OnUpgradeChanged();
        }
    }
    long _attackPower = 0;
    long AttackPower
    {
        get { return _attackPower; }
        set
        {
            if (_attackPower == value)
                return;
            _attackPower = value;
            AttackPowerText.text = _attackPower.ToString();
        }
    }
    long _health = 0;
    long Health
    {
        get { return _health; }
        set
        {
            if (_health == value)
                return;
            _health = value;
            HealthText.text = _health.ToString();
        }
    }
    long _defensePower = 0;
    long DefensePower
    {
        get { return _defensePower; }
        set
        {
            if (_defensePower == value)
                return;
            _defensePower = value;
            DefensePowerText.text = _defensePower.ToString();
        }
    }
    #endregion
    public void Init(AllyData data)
    {
        _data = data;
        CharId = data.Id;
        Lv = 1;
        AllyName.text = _data.name;
        AttackPower = _data.Attack;
        Health = _data.Health;
        DefensePower = _data.Defense;
        UpgradeCostText.text = _data.UpgradeCost.ToString();
        LvUpCostText.text = _data.UpgradeCost.ToString();
    }
    public void OnClickUpgrade()
    {
        if (Manager.Game.Gold < _data.UpgradeCost)
            return;
        Manager.Game.Gold -= _data.UpgradeCost;
        Upgrade++;
    }
    public void OnClickLvUp(bool loaded = false)
    {
        if (Manager.Game.Gold < _data.LvUpCost)
            return;
        Manager.Game.Gold -= _data.LvUpCost;
        Lv++;
    }
    void OnUpgradeChanged()
    {
        var soldier = Manager.Data.playerData.Allys.Allys.Where(s => s.Id == Id).FirstOrDefault();

        if (soldier == null) return;
        soldier.Upgrade = Upgrade;
        Manager.Data.Save();
    }
    public void Openable()
    {
        if (Manager.Game.Gold >= _data.UpgradeCost)
            UpgradeBtn.interactable = true;
        else
            UpgradeBtn.interactable = false;
        if (Manager.Game.Gold >= _data.LvUpCost)
            LvUpBtn.interactable = true;
        else
            LvUpBtn.interactable = false;
    }
}
