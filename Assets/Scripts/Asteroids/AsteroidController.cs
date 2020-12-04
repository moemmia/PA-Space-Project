using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class AsteroidController : MonoBehaviour {

    [SerializeField]
    protected float despawnDistance = 1000;
    
    [SerializeField]
    protected int realDamage = 5;

    [SerializeField]
    protected int shieldsDamage = 1;

    protected Rigidbody _rb;
    protected Transform _target;
    protected Transform _transform;
    protected Health _health;

    public bool isAlive { get => _health.IsAlive; }

    void Awake() {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _target = Player.instance.GetComponent<Transform>();
        _health = GetComponent<Health>();
    }

    void FixedUpdate() {
        if (Vector3.Magnitude(_transform.position - _target.position) > despawnDistance) {
            PoolManager.instance.Despawn(gameObject);
        }
    }

    void OnEnable() {
        StartCoroutine(Appear());
    }

    void OnDisable() {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision col) {
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

    public void OnDie() {
        PoolManager.instance.Despawn(gameObject);
    }

}
