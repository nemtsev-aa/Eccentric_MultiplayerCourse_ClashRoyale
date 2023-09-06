using UnityEngine;

[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour, IHealth {
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float Radius { get; private set; } = 2f;
    [field: SerializeField] public bool IsEnemy { get; private set; } = false;

    private void Start() {
        Health.Init(this);
    }

    public float GetDistance(in Vector3 point) => Vector3.Distance(transform.position, point) - Radius;
}
