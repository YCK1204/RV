using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CreatureController
{
    public void Init(EnemyData data)
    {
        HP = data.Health + data.StageHealth * (Manager.Data.playerData.Stage);
        Attack = data.Attack + data.StageAttack * (Manager.Data.playerData.Stage);
        Defense = data.Defense + data.StageDefense * (Manager.Data.playerData.Stage);
        Speed = data.Speed;
        if (_circleCollider2d == null)
            _circleCollider2d = GetComponent<CircleCollider2D>();
        _circleCollider2d.radius = data.AttackRange;
        Dir = Vector2.left;
        var pos = transform.position;
        pos.y = data.SpawnPosY;
        transform.position = pos;
    }
}
