using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshMove", menuName = "UnitState/NavMeshMove")]
public class NavMeshMove : UnitState {
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private bool _targetIsEnemy;
    private Tower _nearestTower;

    public override void Construct(Unit unit) {
        base.Construct(unit);
        _targetIsEnemy = _unit.IsEnemy == false;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"На персонаже {_unit.name} нет компанента NavMeshAgent");
        _agent.radius = _unit.Parameters.ModelRadius;
        _agent.stoppingDistance = _unit.Parameters.StartAttackDistance;
        _agent.speed = _unit.Parameters.Speed;

    }

    public override void Init() {
        Vector3 unitPosition = _unit.transform.position;
        _nearestTower = MapInfo.Instance.GetNearestTower(in unitPosition, _unit.IsEnemy == false);
        _targetPosition = _nearestTower.transform.position;
        _agent.SetDestination(_targetPosition);
    }

    public override void Run() {
        TryAttackTower();
    }

    public override void Finish() {
        _agent.isStopped = true;
    }

    private void TryAttackTower() {
        float distanceToTarget = _nearestTower.GetDistance(_unit.transform.position); 
        if (distanceToTarget <= _unit.Parameters.StartAttackDistance) {
            Debug.Log("Добежал!");
            _unit.SetState(UnitStateType.Attack);
        }
    }
}
