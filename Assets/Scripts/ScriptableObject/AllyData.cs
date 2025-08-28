using UnityEngine;

[CreateAssetMenu(fileName = "AllyData", menuName = "ScriptableObjects/AllyData", order = 2)]
public class AllyData : CreatureData
{
    [SerializeField]
    AllyController prefab;
    public AllyController Prefab { get { return prefab; } }
    [SerializeField]
    private long openCost;
    public long OpenCost { get { return openCost; } }
    [SerializeField]
    private long lvUpCost;
    public long LvUpCost { get { return lvUpCost; } }
    [SerializeField]
    private long lvUpHealth;
    public long LvUpHealth { get { return lvUpHealth; } }
    [SerializeField]
    private long lvUpAttack;
    public long LvUpAttack { get { return lvUpAttack; } }
    [SerializeField]
    private long lvUpDefense;
    public long LvUpDefense { get { return lvUpDefense; } }

    [SerializeField]
    private long upgradeCost;
    public long UpgradeCost { get { return upgradeCost; } }
    [SerializeField]
    private long spawnCost;
    public long SpawnCost { get { return spawnCost; } }
    [SerializeField]
    private long baseUpgradeHealth;
    public long BaseUpgradeHealth { get { return baseUpgradeHealth; } }
    [SerializeField]
    private long baseUpgradeAttack;
    public long BaseUpgradeAttack { get { return baseUpgradeAttack; } }
    [SerializeField]
    private long baseUpgradeDefense;
    public long BaseUpgradeDefense { get { return baseUpgradeDefense; } }
}
