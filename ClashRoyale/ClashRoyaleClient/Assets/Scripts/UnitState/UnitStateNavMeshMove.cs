using UnityEngine;
using UnityEngine.AI;

public abstract class UnitStateNavMeshMove : UnitState {
    private NavMeshAgent _agent;
    protected bool _targetIsEnemy;
    protected Tower _nearestTower;
    protected MapInfo _mapInfo;

    public override void Construct(Unit unit) {
        base.Construct(unit);
        _targetIsEnemy = _unit.IsEnemy == false;
        _mapInfo = MapInfo.Instance;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"На персонаже {_unit.name} нет компанента NavMeshAgent");
        _agent.radius = _unit.Parameters.ModelRadius;
        _agent.stoppingDistance = _unit.Parameters.StartAttackDistance;
        _agent.speed = _unit.Parameters.Speed;
    }

    public override void Init() {
        Vector3 unitPosition = _unit.transform.position;
        _nearestTower = MapInfo.Instance.GetNearestTower(in unitPosition, _unit.IsEnemy == false);
        Vector3 targetPosition = _nearestTower.transform.position;
        _agent.SetDestination(targetPosition);
    }

    public override void Run() {
        if(TryFindTarget(out UnitStateType changedType)) {
            _unit.SetState(changedType);
        }
    }

    public override void Finish() {
        _agent.SetDestination(_unit.transform.position);
    }

    protected abstract bool TryFindTarget(out UnitStateType changedType);

}
