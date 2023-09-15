using UnityEngine;

public class UnitAnimation : MonoBehaviour {
    private const string _state = "State";
    private const string _attackSpeed = "AttackSpeed";
    private const string _moveSpeed = "MoveSpeed";
    [SerializeField] private Animator _animator;

    public void Init(Unit unit) {
        float damageDelay = unit.Parameters.DamageDelay;
        float moveDelay = unit.Parameters.Speed;
        _animator.SetFloat(_attackSpeed, 1 / damageDelay);
        _animator.SetFloat(_moveSpeed, moveDelay);
    }

    public void SetState(UnitStateType type) {
        _animator.SetInteger(_state, (int)type);
    }
}
