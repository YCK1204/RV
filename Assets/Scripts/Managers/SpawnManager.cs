using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class IgnoreCollision
{
    public LayerMask collider1;
    public LayerMask collider2;
}
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject AllySpawnPos;
    [SerializeField]
    GameObject EnemySpawnPos;
    [SerializeField]
    List<IgnoreCollision> IgnoreCollisions = new List<IgnoreCollision>();

    List<AllyController> Allys = new List<AllyController>();
    List<EnemyController> Enemies = new List<EnemyController>();
    private void Start()
    {
        Init();
    }
    void Init()
    {
        Manager.Spawn = this;
        foreach (var ic in IgnoreCollisions)
        {
            var layers1 = LayerMaskToLayers(ic.collider1);
            var layers2 = LayerMaskToLayers(ic.collider2);
            foreach (var l1 in layers1)
            {
                foreach (var l2 in layers2)
                    Physics2D.IgnoreLayerCollision(l1, l2, true);
            }
        }
    }
    private static int[] LayerMaskToLayers(LayerMask mask)
    {
        var layers = new List<int>();
        int value = mask.value;

        for (int i = 0; i < 32; i++)
        {
            if ((value & (1 << i)) != 0)
                layers.Add(i);
        }

        return layers.ToArray();
    }
    public void SpawnAllys()
    {
        foreach (var soldierData in Manager.Data.playerData.Allys.Allys)
        {
            var sc = soldierData.Clone();
            if (sc == null)
                continue;
            var pos = sc.transform.position;
            pos.x = AllySpawnPos.transform.position.x;
            sc.transform.position = pos;
            Allys.Add(sc);
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
            pos.x = EnemySpawnPos.transform.position.x;
            ec.transform.position = pos;
            Enemies.Add(ec);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAllys();
            SpawnEnemies();
        }
    }
    public void ClearAll()
    {
        foreach (var soldier in Allys)
            Destroy(soldier.gameObject);
        Allys.Clear();
        foreach (var enemy in Enemies)
            Destroy(enemy.gameObject);
        Enemies.Clear();
    }
}
