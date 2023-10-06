using UnityEngine;

public class ArrowCreator : MonoBehaviour {
    [SerializeField] private Arrow _arrow;
    [SerializeField] private Unit _unit;
    private IHealth _target;

    public void Init(Unit unit) {
        _unit = unit;
        _unit.OnTargetChanged += SetTarget;
    }

    private void SetTarget(IHealth target) {
        _target = target;
    }

    public void Create() {
        Arrow arrow = Instantiate(_arrow, transform.position, transform.rotation);
        arrow.Init(_target);
    }
}
