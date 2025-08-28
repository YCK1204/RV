using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISoldier : MonoBehaviour
{
    [SerializeField]
    Image SoldierIcon;
    [SerializeField]
    TextMeshProUGUI UpgradeText;
    [SerializeField]
    TextMeshProUGUI LevelText;

    [SerializeField]
    TextMeshProUGUI SoldierName;
    [SerializeField]
    TextMeshProUGUI AttackPowerText;
    [SerializeField]
    TextMeshProUGUI HealthText;
    [SerializeField]
    TextMeshProUGUI DefensePowerText;

    [SerializeField]
    Button SpawnBtn;
    [SerializeField]
    TextMeshProUGUI SpawnCostText;

    [SerializeField]
    Button OpenOrLvUpBtn;
    [SerializeField]
    TextMeshProUGUI LvUpCostText;
    [SerializeField]
    TextMeshProUGUI OpenCostText;

    #region data
    SoldierData _data;

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
            OnLvOrUpgradeChanged();
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
            OnLvOrUpgradeChanged();
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
    [HideInInspector]
    #endregion
    public bool IsActive = false;
    public void Init(SoldierData data)
    {
        _data = data;
        CharId = data.CharId;
        Lv = 1;
        SoldierName.text = _data.name;
        AttackPower = _data.Attack;
        Health = _data.Health;
        DefensePower = _data.Defense;
        SpawnCostText.text = _data.SpawnCost.ToString();
        LvUpCostText.text = _data.UpgradeCost.ToString();
        OpenCostText.text = _data.OpenCost.ToString();
    }
    public bool Openable()
    {
        bool enable = true;

        if (IsActive)
        {
            enable &= Manager.Game.Gold >= _data.SpawnCost;
            SpawnBtn.interactable = enable;
            enable &= Manager.Game.Gold >= _data.UpgradeCost;
            OpenOrLvUpBtn.interactable = enable;
        }
        else
        {
            enable &= Manager.Game.Gold >= _data.OpenCost;
            OpenOrLvUpBtn.interactable = enable;
        }
        return enable;
    }
    public void OnClickSpawn()
    {
        if (!IsActive)
            return;
        if (Manager.Game.Gold < _data.SpawnCost)
            return;
        Manager.Game.Gold -= _data.SpawnCost;
        // spawn
    }
    public void OnClickOpenOrLvUp(bool loaded = false)
    {
        if (IsActive)
        {
            if (Manager.Game.Gold < _data.UpgradeCost)
                return;
            Manager.Game.Gold -= _data.UpgradeCost;
            Lv++;
        }
        else
        {
            if (loaded == false)
            {
                if (Manager.Game.Gold < _data.OpenCost)
                    return;
                Manager.Game.Gold -= _data.OpenCost;
            }
            IsActive = true;
            OpenCostText.gameObject.SetActive(false);
            LvUpCostText.gameObject.SetActive(true);
            //Openable();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Manager.UI.Soldier.Add(this);
        }
    }
    void OnLvOrUpgradeChanged()
    {
        var soldier = Manager.Data.playerData.Soldiers.Soldiers.Where(s => s.Id == Id).FirstOrDefault();

        if (soldier == null) return;
        soldier.Level = Lv;
        soldier.Upgrade = Upgrade;
        Manager.Data.Save();
    }
}
