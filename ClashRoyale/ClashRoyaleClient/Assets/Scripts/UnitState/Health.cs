using System;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private HealtView _healthUI;
    [SerializeField] private ParticleSystem _destroyParticle;

    [field: SerializeField] public float Max { get; private set; } = 10f;
    private float _current;
    private MapInfo _mapInfo;
    private IHealth _gameObject;

    public void Init(IHealth gameObject) {
        _current = Max;
        _mapInfo = MapInfo.Instance;
        _gameObject = gameObject;
    }

    public void SetMax(int max) {
        Max = max;
        UpdateHP();
    }

    public void SetCurrent(int current) {
        _current = current;
        UpdateHP();
    }

    public void ApplyDamage(float value) {
        _current -= value;
        if (_current < 0) {
            _current = 0;
            Destroy();
        } else {
            UpdateHP();
        }
    }

    private void UpdateHP() {
        _healthUI.UpdateHealth(Max, _current);
    }

    public void AddHealth(int lootValue) {
        _current += lootValue;
        if (_current > Max) _current = Max;
        UpdateHP();
    }

    private void Destroy() {
        Destroy(gameObject);
        Instantiate(_destroyParticle, transform.position, Quaternion.identity);
        _mapInfo.RemoveObject(_gameObject, _gameObject.IsEnemy);
    }
}

public interface IHealth {
   Health Health { get; }
   bool IsEnemy { get; }
}
