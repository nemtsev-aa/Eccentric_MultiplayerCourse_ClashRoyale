using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _lifeTime;

    private string _gunslingerID;
    private IHealth _target;
    private float _damage;

    public void Init(IHealth target, Vector3 velocity, float damage = 0, string gunslingerID = "") {
        _target = target;
        _damage = damage;
        _gunslingerID = gunslingerID;
        _rigidbody.velocity = velocity;
        StartCoroutine(DelayDestroy());
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody == null) {
            Destroy();
            return;
        }

        if (collision.rigidbody.gameObject.TryGetComponent(out Unit unit)) {
            if (unit.IsEnemy == false) return;
            else {
                unit.Health.ApplyDamage(_damage);
                Destroy();
            }
        }

        if (collision.rigidbody.gameObject.TryGetComponent(out Tower tower)) {
            if (tower.IsEnemy == false) return;
            else {
                tower.Health.ApplyDamage(_damage);
                Destroy();
            }
        }
    }

    private IEnumerator DelayDestroy() {
        yield return new WaitForSecondsRealtime(_lifeTime);
        Destroy();
    }

    private void Destroy() {
        Destroy(gameObject);
    }

    //public void Update() {
    //    Vector3 targetPosition = _target.Health.gameObject.transform.position + Vector3.up * 0.5f;


    //    float distance = Vector3.Distance(transform.position, targetPosition);
    //    if (distance > 0.1f) {
    //        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.1f);
    //    } else {
    //        if (_target.Health != null) {
    //            _target.Health.ApplyDamage(_damage);
    //        }
    //        Destroy(gameObject);
    //    }
    //}
}
