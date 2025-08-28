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
    protected GameObject _target;
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
    public abstract void OnAttack();

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
            transform.position += (Vector3)(Dir.normalized * Speed * Time.deltaTime);
        }
    }
    protected virtual void UpdateAttack()
    {
        if (_target == null)
            State = CreatureState.Walk;
    }
    protected virtual void UpdateDie()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_target != null)
            return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Aura"))
            return;
        var distance = Vector2.Distance(collision.gameObject.transform.position, transform.position);
        if (distance > _circleCollider2d.radius)
            return;
        _target = collision.gameObject;
        State = CreatureState.Attack;
    }
    public void OnDied()
    {
        Manager.Resource.Destroy(gameObject);
    }
}
