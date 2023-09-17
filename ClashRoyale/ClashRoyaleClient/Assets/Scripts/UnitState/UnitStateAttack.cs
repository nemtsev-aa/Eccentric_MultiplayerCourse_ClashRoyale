using UnityEngine;

public abstract class UnitStateAttack : UnitState {
    [field: SerializeField] protected float Damage { get; private set; } = 1.5f;
    private float _delay;
    private float _time;

    private float _stopAttackDistance = 0f;
    protected bool _targetIsEnemy;
    protected MapInfo _mapInfo;
    protected Health _target;

    public override void Construct(Unit unit) {
        base.Construct(unit);
        _targetIsEnemy = _unit.IsEnemy == false;
        _delay = _unit.Parameters.DamageDelay;
        _mapInfo = MapInfo.Instance;
    }

    public override void Init() {
        if (TryFindTarget(out _stopAttackDistance) == false) {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        _time = 0f;
        _unit.transform.LookAt(_target.transform.position);
    }

    public override void Run() {
        _time += Time.deltaTime;
        if (_time < _delay) return;
        _time -= _delay;

        if (_target == false) {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        float distanceToTarget = Vector3.Distance(_target.transform.position, _unit.transform.position);
        if (distanceToTarget > _stopAttackDistance) {
            _unit.SetState(UnitStateType.Chase);
        } else {
            Attack();
        }
    }

    protected virtual void Attack() {
        _target.ApplyDamage(Damage);
    }

    public override void Finish() {

    }

    protected abstract bool TryFindTarget(out float stopAttackDistance);
}
