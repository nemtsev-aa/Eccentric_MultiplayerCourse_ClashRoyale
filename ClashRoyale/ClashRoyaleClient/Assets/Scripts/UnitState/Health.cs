using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
    [field: SerializeField] public float Max { get; private set; } = 10f;
    public float Current => _current;
    [SerializeField] private HealthView _healthUI;

    public event Action<float, float> UpdateHealth;

    private float _current;
    private MapInfo _mapInfo;
    private IHealth _gameObject;

    public void Init(IHealth gameObject) {
        _current = Max;
        _mapInfo = MapInfo.Instance;
        _gameObject = gameObject;
        _healthUI.Init();
        UpdateHealth += _healthUI.UpdateHealth;
    }

    public void ApplyDamage(float value) {
        _current -= value;
        if (_current < 0) _current = 0;
        UpdateHP();
    }

    public void ApplyDelayDamage(float delay, float damage) {
        StartCoroutine(DelayDamage(delay, damage));
    }

    private IEnumerator DelayDamage(float delay, float damage) {
        yield return new WaitForSeconds(delay);
        ApplyDamage(damage);
    }

    private void UpdateHP() {
        UpdateHealth?.Invoke(_current, Max);
    }

    public void AddHealth(int lootValue) {
        _current += lootValue;
        if (_current > Max) _current = Max;
        UpdateHP();
    }

}

public interface IHealth {
   Health Health { get; }
   bool IsEnemy { get; }
}
