using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class AllyController : CreatureController
{
    public void Init(long level, long upgrade, AllyData data)
    {
        HP = data.BaseUpgradeHealth + (level * data.LvUpHealth) + (upgrade * data.BaseUpgradeHealth);
        Attack = data.BaseUpgradeAttack + (level * data.LvUpAttack) + (upgrade * data.BaseUpgradeAttack);
        Defense = data.BaseUpgradeDefense + (level * data.LvUpDefense) + (upgrade * data.BaseUpgradeDefense);
        Speed = data.Speed;
        if (_circleCollider2d == null)
            _circleCollider2d = GetComponent<CircleCollider2D>();
        _circleCollider2d.radius = data.AttackRange;
        Dir = Vector2.right;
        var pos = transform.position;
        pos.y = data.SpawnPosY;
        transform.position = pos;
    }
}
