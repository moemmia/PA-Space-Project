using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float linearForce = 1000;
    public float despawnDistance = 1000;
    public float angularForce = 1f;

    protected Rigidbody _rg;
    protected Transform _playerTransform;
    protected Transform _transform;
    private EnemyEquipment _weapon;
    protected Vector3 _directionToPlayer;

    public int realDamage = 5;
    public int shieldsDamage = 1;
    protected Health _health;

    void Awake() {
        _rg = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _playerTransform = Player.instance.GetComponent<Transform>();
        _health = GetComponent<Health>();
        _weapon = GetComponent<EnemyEquipment>();
    }

    private void OnEnable() {
        _directionToPlayer = _playerTransform.position - _transform.position;
        var normalizedDirection = _directionToPlayer.normalized;
        _transform.forward = normalizedDirection;
        linearForce -= _transform.localScale.x * 100f;
        angularForce -= _transform.localScale.x * 0.1f;
    }

    private void OnDisable() {
        _rg.velocity = Vector3.zero;
        _rg.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision col) {
        var h = col.collider.GetComponent<Health>();
        if (h) {
            h.TakeDamage(realDamage, shieldsDamage);
            _health.TakeDamage(realDamage, shieldsDamage);
        }
    }

    void FixedUpdate() {
        Vector3 targetDelta = _playerTransform.position - _transform.position;
        float distance = Vector3.Distance(_transform.position,  _playerTransform.position);
        float angle = Vector3.Angle(_transform.forward,  _playerTransform.forward);

        Quaternion targetRotation = Quaternion.LookRotation(targetDelta);
        _rg.MoveRotation(Quaternion.RotateTowards(_transform.rotation, targetRotation, angularForce));

        Vector3 linearInput = _transform.forward;
        Vector3 appliedLinearForce = linearInput * linearForce * distance/despawnDistance;
        _rg.AddForce(appliedLinearForce, ForceMode.Acceleration);

        _weapon.SetShooting(distance <= 100 && angle < 205f && angle > 167f);

        if (_directionToPlayer.magnitude >= despawnDistance) {
            PoolManager.instance.Despawn(gameObject);
        }
    }

    public void OnDie() {
        PoolManager.instance.Despawn(gameObject);
    }
}
