using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public float despawnDistance = 1000;
    protected Rigidbody _rb;
    protected Transform _playerTransform;
    protected Transform _transform;

    void Awake() {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _playerTransform = Player.instance.GetComponent<Transform>();
    }

    void OnEnable() {
        StartCoroutine(Appear());
    }

    void OnDisable() {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    void FixedUpdate() {
        if (Vector3.Magnitude(_transform.position - _playerTransform.position) > despawnDistance) {
            PoolManager.instance.Despawn(gameObject);
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
