using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 3)]
public class EnemyData : CreatureData
{
    [SerializeField]
    EnemyController prefab;
    public EnemyController Prefab { get { return prefab; } }
    [SerializeField]
    private long stageHealth;
    public long StageHealth { get { return stageHealth; } }
    [SerializeField]
    private long stageAttack;
    public long StageAttack { get { return stageAttack; } }
    [SerializeField]
    private long stageDefense;
    public long StageDefense { get { return stageDefense; } }
    public EnemyController Clone()
    {
        if (prefab == null)
            return null;
        var ec = Manager.Resource.Instantiate<EnemyController>(prefab);
        ec.Init(this);
        return ec;
    }
}
