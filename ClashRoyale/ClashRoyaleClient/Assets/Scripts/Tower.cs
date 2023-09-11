using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour, IHealth, IDestroyed {
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float Radius { get; private set; } = 2f;
    [field: SerializeField] public bool IsEnemy { get; private set; } = false;

    [SerializeField] private ParticleSystem _destroyParticle;

    public event Action Destroyed;

    public float GetDistance(in Vector3 point) => Vector3.Distance(transform.position, point) - Radius;
    
    private void Start() {
        Health.Init(this);
        Health.UpdateHealth += CheckDestroy;
    }

    private void CheckDestroy(float currentHP, float maxHP) {
        if (currentHP > 0) return;
        Health.UpdateHealth -= CheckDestroy;

        Destroy(gameObject);
        Instantiate(_destroyParticle, transform.position, Quaternion.identity);

        Destroyed?.Invoke();
    }
}
