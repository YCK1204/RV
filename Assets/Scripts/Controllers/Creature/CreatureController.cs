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
    protected SpriteRenderer[] _spriteRenderers;
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
    long _hp;
    protected long HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp <= 0)
            {
                _hp = 0;
                State = CreatureState.Die;
            }
        }
    }
    protected long Attack { get; set; } = 1;
    protected long Defense { get; set; } = 0;
    protected virtual void Start()
    {
        _circleCollider2d = GetComponent<CircleCollider2D>();
        _boxCollider2d = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        _circleCollider2d.offset = _boxCollider2d.offset;

        _spriteRenderers = transform.FindChilds<SpriteRenderer>(true);
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
    float duration = .5f;
    float timer = 0f;
    protected virtual void UpdateDie()
    {
        if (timer < duration)
        {
            foreach (var renderer in _spriteRenderers)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, timer / duration);
                var color = renderer.color;
                color.a = alpha;
                renderer.color = color;
            }
        }
        else
        {
            foreach (var renderer in _spriteRenderers)
            {
                var color = renderer.color;
                color.a = 0f;
                renderer.color = color;
            }
        }
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
