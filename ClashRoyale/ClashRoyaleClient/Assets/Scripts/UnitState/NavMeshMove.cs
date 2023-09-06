using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshMove", menuName = "UnitState/NavMeshMove")]
public class NavMeshMove : UnitState {
    private NavMeshAgent _agent;
    private bool _targetIsEnemy;
    private Tower _nearestTower;
    private MapInfo _mapInfo;

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
        if (TryAttackTower()) return;
        if (TryAttackUnit()) return;
    }

    public override void Finish() {
        _agent.SetDestination(_unit.transform.position);
    }

    private bool TryAttackTower() {
        float distanceToTarget = _nearestTower.GetDistance(_unit.transform.position); 
        if (distanceToTarget <= _unit.Parameters.StartAttackDistance) {
            _unit.SetState(UnitStateType.Attack);
            return true;
        }
        return false;
    }

    private bool TryAttackUnit() {
        bool hasEnemy = _mapInfo.TryGetNearestUnit(_unit.transform.position, _targetIsEnemy, out Unit enemy, out float distance);
        if (hasEnemy == false) return false;

        if (_unit.Parameters.StartChaseDistance >= distance) {
            _unit.SetState(UnitStateType.Chase);
            return true;
        }
        return false;
    }
}
