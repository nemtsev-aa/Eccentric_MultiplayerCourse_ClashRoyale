using UnityEngine;

[CreateAssetMenu(fileName = "EmptyUnitState", menuName = "UnitState/EmptyUnitState")]
public class EmptyUnitState : UnitState {
    public override void Init() {
        _unit.SetState(UnitStateType.Default);
    }

    public override void Run() {
        
    }

    public override void Finish() {
        Debug.Log($"{_unit.name} ��� � UnitStateType.None, ��� ���������� � UnitStateType.Default");
    }
}
