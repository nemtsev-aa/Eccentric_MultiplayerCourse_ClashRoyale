using UnityEngine;

public class Arrow : MonoBehaviour {
    [field: SerializeField] public float Speed { get; private set; } = 5f;
    public GameObject _target;
    private float _flightAltitude;

    public void Init(IHealth target) {
        _target = target.Health.gameObject;
        transform.LookAt(GetTargetPosition());
        _flightAltitude = transform.position.y;
    }

    private Vector3 GetTargetPosition() {
        if (_target != null) return _target.transform.position;
        else return Vector3.zero;
    }

    void Update() {
        Vector3 targetPosition = GetTargetPosition() + Vector3.up * _flightAltitude;
        if (targetPosition != Vector3.zero && _target != null) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
            if (transform.position == targetPosition) Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
