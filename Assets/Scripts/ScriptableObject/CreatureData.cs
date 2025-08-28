using UnityEngine;

public class CreatureData : ScriptableObject
{
    [SerializeField]
    private int id;
    public int Id { get { return id; } }
    [SerializeField]
    private string name;
    public string Name { get { return name; } }
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
    private float attackRange;
    public float AttackRange { get { return attackRange; } }
    [SerializeField]
    private float spawnPosY;
    public float SpawnPosY { get { return spawnPosY; } }
}
