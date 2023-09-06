using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [field: SerializeField] public float Max { get; private set; } = 10f;
    private float _current;

    private void Start() {
        _current = Max;
    }

    public void ApplyDamage(float value) {
        _current -= value;
        if (_current < 0) _current = 0;
        Debug.Log($"������ {name}: ���� {_current + value} ����� {_current}");
    }
}

interface IHealth {
    Health Health { get; }
}
