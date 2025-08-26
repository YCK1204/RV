#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierData", menuName = "ScriptableObjects/SoldierData", order = 2)]
public class SoldierData : ScriptableObject
{
    [SerializeField]
    private string soldierName;
    public string SoldierName { get { return soldierName; } }
    [SerializeField]
    private long health;
    public long Health { get { return health; } }
    [SerializeField]
    private long attack;
    public long Attack { get { return attack; } }
    [SerializeField]
    private long defense;
    public long Defense { get { return defense; } }
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }
    [SerializeField]
    private AnimatorController soldierAnimController;
    public AnimatorController SoldierAnimController { get { return soldierAnimController; } }
    [SerializeField]
    private float attackRange;
    public float AttackRange { get { return attackRange; } }
    [SerializeField]
    private long openCost;
    public long OpenCost { get { return openCost; } }

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
