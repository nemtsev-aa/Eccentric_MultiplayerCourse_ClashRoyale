using System;
using UnityEngine;

[CreateAssetMenu(fileName = "_UsualMeleeAttack", menuName = "UnitState/UsualMeleeAttack")]
public class UsualMeleeAttack : UnitStateAttack {
    public Action<IHealth, float> OnTargetChanged;

    protected override bool TryFindTarget(out float stopAttackDistance) {
        Vector3 unitPosition = _unit.transform.position;

        bool hasEnemy = _mapInfo.TryGetNearestWalkingUnit(unitPosition, _targetIsEnemy, out Unit enemy, out float distance);

        if (hasEnemy && distance - enemy.Parameters.ModelRadius <= _unit.Parameters.StartAttackDistance) {
            _target = enemy.Health;
            stopAttackDistance = _unit.Parameters.StopAttackDistance + enemy.Parameters.ModelRadius;
            _unit.OnTargetChanged?.Invoke(enemy);
            return true;
        }

        Tower targetTower = _mapInfo.GetNearestTower(unitPosition, _targetIsEnemy);
        if (targetTower.GetDistance(unitPosition) <= _unit.Parameters.StartAttackDistance) {
            _target = targetTower.Health;
            stopAttackDistance = _unit.Parameters.StopAttackDistance + targetTower.Radius;
            _unit.OnTargetChanged?.Invoke(targetTower);
            return true;
        }

        stopAttackDistance = 0f;
        return false;
    }
}
