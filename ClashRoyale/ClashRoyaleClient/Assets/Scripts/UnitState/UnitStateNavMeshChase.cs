using UnityEditor;
using UnityEngine;

public abstract class UnitStateNavMeshChase : UnitState {
    private UnityEngine.AI.NavMeshAgent _agent;
    protected bool _targetIsEnemy;
    protected Unit _targetUnit;
    protected float _startAttackDistance = 0f;

    public override void Construct(Unit unit) {
        base.Construct(unit);
        _targetIsEnemy = _unit.IsEnemy == false;

        _agent = _unit.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (_agent == null) Debug.LogError($"На персонаже {_unit.name} нет компанента NavMeshAgent");
    }

    public override void Init() {
        FindTargetUnit(out _targetUnit);
        if (_targetUnit == null) {
            _unit.SetState(UnitStateType.Default);
            return;
        }
         _startAttackDistance = _unit.Parameters.StartAttackDistance + _targetUnit.Parameters.ModelRadius;
    }

    public override void Run() {
        float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetUnit.transform.position);
        if (distanceToTarget > _unit.Parameters.StopChaseDistance) _unit.SetState(UnitStateType.Default);
        else if (distanceToTarget <= _startAttackDistance) _unit.SetState(UnitStateType.Attack);
        else _agent.SetDestination(_targetUnit.transform.position);
    }

    public override void Finish() {
        _agent.SetDestination(_unit.transform.position);
    }

#if UNITY_EDITOR
    public override void DebugDrawDistance(Unit unit) {
        Handles.color = Color.red;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.StartChaseDistance);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.StopChaseDistance);
    }
#endif

    protected abstract void FindTargetUnit(out Unit targetUnit);
}
