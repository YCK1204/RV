using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodGolemController : EnemyController
{
    public override void OnAttack()
    {
        if (_target == null) return;
        _target.TakeDamage(Attack);
    }
}
