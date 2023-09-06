using UnityEngine;
[RequireComponent(typeof(UnitParameters), typeof(Health))]
public class Unit : MonoBehaviour, IHealth {
    [field: SerializeField] public Health Health {get; private set;}
    [field: SerializeField] public bool IsEnemy { get; private set; } = false;
    [field: SerializeField] public UnitParameters Parameters;

    [SerializeField] private UnitState _defaultStateSO;
    [SerializeField] private UnitState _chaseStateSO;
    [SerializeField] private UnitState _attackStateSO;

    private UnitState _defaultState;
    private UnitState _chaseState;
    private UnitState _attackState;

    private UnitState _currentState;

    private void Start() {
        _defaultState = Instantiate(_defaultStateSO);
        _defaultState.Construct(this);
        _chaseState = Instantiate(_chaseStateSO);
        _chaseState.Construct(this);
        _attackState = Instantiate(_attackStateSO);
        _attackState.Construct(this);

        _currentState = _defaultState;
        _currentState.Init();
    }

    private void Update() {
        _currentState.Run();
    }

    public void SetState(UnitStateType type) {
        _currentState.Finish();
        switch (type) {
            case UnitStateType.None:
                break;
            case UnitStateType.Default:
                _currentState = _defaultState;
                break;
            case UnitStateType.Chase:
                _currentState = _chaseState;
                break;
            case UnitStateType.Attack:
                _currentState = _attackState;
                break;
            default:
                Debug.Log($"Не обрабатывается состояние {type}!");
                break;
        }
        _currentState.Init();
    }

#if UNITY_EDITOR
    [Space(10)]
    [SerializeField] private bool _debug = false; 
    private void OnDrawGizmos() {
        if (!_debug) return;
        if (_chaseStateSO != null) _chaseStateSO.DebugDrawDistance(this);
    }
#endif
}
