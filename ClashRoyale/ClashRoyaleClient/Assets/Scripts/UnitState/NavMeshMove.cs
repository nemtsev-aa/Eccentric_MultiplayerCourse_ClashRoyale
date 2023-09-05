using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshMove", menuName = "UnitState/NavMeshMove")]
public class NavMeshMove : UnitState {
    

    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private bool _targetIsEnemy;

    public override void Construct(Unit unit) {
        base.Construct(unit);
        _targetIsEnemy = _unit.IsEnemy == false;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"На персонаже {_unit.name} нет компанента NavMeshAgent");

        _agent.stoppingDistance = _unit.Parameters.StartAttackDistance;
        _agent.speed = _unit.Parameters.Speed;

    }

    public override void Init() {
        Vector3 unitPosition = _unit.transform.position;
        _targetPosition = MapInfo.Instance.GetNearestTowerPosition(in unitPosition, _unit.IsEnemy == false);
        _agent.SetDestination(_targetPosition);
    }

    public override void Run() {
        float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetPosition);
        if (distanceToTarget <= _unit.Parameters.StartAttackDistance) {
            Debug.Log("Добежал!");
            _unit.SetState(UnitStateType.Attack);
        }
    }

    public override void Finish() {
        _agent.isStopped = true;
    }
}
