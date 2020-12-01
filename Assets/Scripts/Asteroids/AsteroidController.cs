using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public float despawnDistance = 1000;
    protected Rigidbody _rb;
    protected Transform _playerTransform;
    protected Transform _transform;
    protected Health _health;
    public bool isAlive { get => _health.IsAlive; }
    public int realDamage = 5;
    public int shieldsDamage = 1;

    void Awake() {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _playerTransform = Player.instance.GetComponent<Transform>();
        _health = GetComponent<Health>();
    }

    void OnEnable() {
        StartCoroutine(Appear());
    }

    void OnDisable() {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public void OnDie() {
        PoolManager.instance.Despawn(gameObject);
    }

    void FixedUpdate() {
        if (Vector3.Magnitude(_transform.position - _playerTransform.position) > despawnDistance) {
            PoolManager.instance.Despawn(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col) {
        var h = col.collider.GetComponent<Health>();
        if (h) {
            h.TakeDamage(realDamage, shieldsDamage);
            _health.TakeDamage(realDamage, shieldsDamage);
        }
    }


    protected IEnumerator Appear() {
        Vector3 scale = _transform.localScale;
        for (int i = 0; i < 10; i++) {
            _transform.localScale = scale * i/10;
            yield return new WaitForSeconds(.1f);
        }
    }
}
