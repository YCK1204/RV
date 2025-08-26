using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/QuestData", order = 1)]
public class QuestData : ScriptableObject
{
    [SerializeField]
    Sprite _icon;
    public Sprite Icon { get { return _icon; } }
    [SerializeField]
    string _questName;
    public string QuestName { get { return _questName; } }
    [SerializeField]
    long _baseGoldReward;
    public long BaseGoldReward { get { return _baseGoldReward; } }
    [SerializeField]
    float _baseCooldown;
    public float BaseCooldown { get { return _baseCooldown; } }
    [SerializeField]
    long _upgradeCost;
    public long UpgradeCost { get { return _upgradeCost; } }
    [SerializeField]
    long _extraGoldPerUpgrade;
    public long ExtraGoldPerUpgrade { get { return _extraGoldPerUpgrade; } }
    [SerializeField]
    long _openCost;
    public long OpenCost { get { return _openCost; } }
}
