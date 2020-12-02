using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float despawnDistance = 1000f;

    public int realDamage = 5;
    public int shieldsDamage = 1;

    private EnemyEquipment _weapon;
    protected Vector3 _directionToPlayer;

    protected EnemyPhysics _physics;
    protected Health _health;

    protected EnemyInput _input;

    void Awake() {
        _health = GetComponent<Health>();
        _weapon = GetComponent<EnemyEquipment>();
        _input = GetComponent<EnemyInput>();
        _physics = GetComponent<EnemyPhysics>();
    }

    private void OnCollisionEnter(Collision col) {
        var h = col.collider.GetComponent<Health>();
        if (h) {
            h.TakeDamage(realDamage, shieldsDamage);
            _health.TakeDamage(realDamage, shieldsDamage);
        }
    }

    void Update() {
        _physics.SetPhysicsInput(_input.linearInput, _input.angularInput);
        
        _weapon.SetShooting(_input.isShooting);

        if (_input.targetDistance >= despawnDistance) {
            PoolManager.instance.Despawn(gameObject);
        }
    }

    public void OnDie() {
        PoolManager.instance.Despawn(gameObject);
    }
}
