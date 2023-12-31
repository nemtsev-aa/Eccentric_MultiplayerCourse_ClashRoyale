using System;
using UnityEngine;

[RequireComponent(typeof(UnitParameters), typeof(Health), typeof(UnitParameters))]
public class Unit : MonoBehaviour, IHealth, IDestroyed {
    [field: SerializeField] public Health Health {get; private set;}
    [field: SerializeField] public bool IsEnemy { get; private set; } = false;
    [field: SerializeField] public UnitParameters Parameters;
    
    public event Action Destroyed;
    public Action<IHealth> OnTargetChanged;
    
    [SerializeField] private UnitAnimation _unitAnimation;
    [SerializeField] private UnitState _defaultStateSO;
    [SerializeField] private UnitState _chaseStateSO;
    [SerializeField] private UnitState _attackStateSO;
    [SerializeField] private ParticleSystem _destroyParticle;

    [Space(10)]
    [SerializeField] private ArrowCreator _arrowCreator;

    private UnitState _defaultState;
    private UnitState _chaseState;
    private UnitState _attackState;
    private UnitState _currentState;

    private void Start() {
        CreateState();

        _currentState = _defaultState;
        _currentState.Init();

        Health.Init(this);
        Health.UpdateHealth += CheckDestroy;

        _unitAnimation.Init(this);

        if (_arrowCreator != null) _arrowCreator.Init(this);
    }
    
    private void CreateState() {
        _defaultState = Instantiate(_defaultStateSO);
        _defaultState.Construct(this);
        _chaseState = Instantiate(_chaseStateSO);
        _chaseState.Construct(this);
        _attackState = Instantiate(_attackStateSO);
        _attackState.Construct(this);
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
        _unitAnimation.SetState(type);
    }

    private void CheckDestroy(float currentHP, float maxHP) {
        if (currentHP > 0) return;
        Health.UpdateHealth -= CheckDestroy;

        Destroyed?.Invoke();
    }

    public void DestroyUnit() {
        if (Health.Current == 0) {
            Destroy(gameObject);
            Instantiate(_destroyParticle, transform.position, Quaternion.identity);
        }
    }

    public UsualRangeAttack GetUsualRangeAttack() {
        return (UsualRangeAttack)_attackState;
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
