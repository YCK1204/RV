using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureController : MonoBehaviour
{
    protected enum CreatureState
    {
        Walk,
        Attack,
        Die
    }
    CreatureState _state;
    Animator _animator;
    protected CircleCollider2D _circleCollider2d;
    protected BoxCollider2D _boxCollider2d;
    protected CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state != value)
            {
                _state = value;
                string animName = "";
                bool needReset = false;
                switch (_state)
                {
                    case CreatureState.Walk:
                        animName = "IsAttack";
                        needReset = false;
                        break;
                    case CreatureState.Attack:
                        animName = "IsAttack";
                        needReset = true;
                        break;
                    case CreatureState.Die:
                        animName = "IsDied";
                        needReset = true;
                        break;
                }
                _animator.SetBool(animName, needReset);
            }
        }
    }
    protected Vector2 Dir { get; set; } = Vector2.zero;
    protected float Speed { get; set; } = 1f;
    protected long HP { get; set; } = 1;
    protected long Attack { get; set; } = 1;
    protected long Defense { get; set; } = 0;
    protected virtual void Start()
    {
        _circleCollider2d = GetComponent<CircleCollider2D>();
        _boxCollider2d = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        _circleCollider2d.offset = _boxCollider2d.offset;
    }
    void Update()
    {
        UpdateController();
    }
    protected virtual void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Walk:
                UpdateWalk();
                break;
            case CreatureState.Attack:
                UpdateAttack();
                break;
            case CreatureState.Die:
                UpdateDie();
                break;
        }
    }
    protected virtual void UpdateWalk()
    {
        if (Dir != Vector2.zero)
        {
        }
    }
    protected virtual void UpdateAttack()
    {
    }
    protected virtual void UpdateDie()
    {
    }
    public void OnDied()
    {
        Manager.Resource.Destroy(gameObject);
    }
}
