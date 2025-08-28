using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    Vector2 SoldierSpawnPos;
    [SerializeField]
    Vector2 EnemySpawnPos;

    List<SoldierController> Soldiers = new List<SoldierController>();
    List<EnemyController> Enemies = new List<EnemyController>();
    private void Start()
    {
        Init();
    }
    void Init()
    {
        Manager.Spawn = this;
        SpawnSoldiers();
        SpawnEnemies();
    }
    public void SpawnSoldiers()
    {
        foreach (var soldierData in Manager.Data.playerData.Soldiers.Soldiers)
        {
            var sc = soldierData.Clone();
            if (sc == null)
                continue;
            var pos = sc.transform.position;
            pos.x = SoldierSpawnPos.x;
            sc.transform.position = pos;
            Soldiers.Add(sc);
        }
    }
    public void SpawnEnemies()
    {
        var enemySize = Manager.Data.EnemyData.Count;
        var ranSize = Random.Range(enemySize / 2, enemySize + 1);

        var enemyList = Manager.Data.EnemyData.ToList();
        for (int i = 0; i < ranSize; i++)
        {
            var ec = enemyList[Random.Range(0, enemySize)].Value.Clone();
            var pos = ec.transform.position;
            pos.x = EnemySpawnPos.x;
            ec.transform.position = pos;
            Enemies.Add(ec);
        }
    }
    public void ClearAll()
    {
        foreach (var soldier in Soldiers)
            Destroy(soldier.gameObject);
        Soldiers.Clear();
        foreach (var enemy in Enemies)
            Destroy(enemy.gameObject);
        Enemies.Clear();
    }
}
