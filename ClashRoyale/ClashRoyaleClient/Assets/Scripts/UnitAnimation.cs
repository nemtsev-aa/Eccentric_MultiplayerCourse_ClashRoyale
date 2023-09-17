using System;
using UnityEngine;

public class UnitAnimation : MonoBehaviour {
    private const string _state = "State";
    private const string _attackSpeed = "AttackSpeed";
    private const string _moveSpeed = "MoveSpeed";
    private const string _die = "Die";

    [SerializeField] private Animator _animator;
    private Unit _unit;

    public void Init(Unit unit) {
        _unit = unit;
        float damageDelay = unit.Parameters.DamageDelay;
        float moveDelay = unit.Parameters.Speed;
        _animator.SetFloat(_attackSpeed, 1 / damageDelay);
        _animator.SetFloat(_moveSpeed, moveDelay);
        _unit.Destroyed += DestroyUnit;
    }

    public void SetState(UnitStateType type) {
        _animator.SetInteger(_state, (int)type);
    }

    private void DestroyUnit() {
        _animator.SetTrigger(_die);
    }

    private void OnDestroy() {
        _unit.Destroyed += DestroyUnit;
    }
}
