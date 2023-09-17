using UnityEngine;

[CreateAssetMenu(fileName = "_UsualTowerAttack", menuName = "UnitState/UsualTowerAttack")]
public class UsualTowerAttack : UnitStateAttack {
    protected override bool TryFindTarget(out float stopAttackDistance) {
        Vector3 unitPosition = _unit.transform.position;

        Tower targetTower = _mapInfo.GetNearestTower(unitPosition, _targetIsEnemy);
        if (targetTower.GetDistance(unitPosition) <= _unit.Parameters.StartAttackDistance) {
            _target = targetTower.Health;
            stopAttackDistance = _unit.Parameters.StopAttackDistance + targetTower.Radius;
            return true;
        }

        stopAttackDistance = 0f;
        return false;
    }
}
