using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyPhysics))]
[RequireComponent(typeof(EnemyInput))]
[RequireComponent(typeof(EnemyEquipment))]
public class Enemy : MonoBehaviour {

    [SerializeField]
    protected float despawnDistance = 1000f;

    [SerializeField]
    protected int realDamage = 5;
    
    [SerializeField]
    protected int shieldsDamage = 1;

    protected EnemyEquipment _weapon;
    protected EnemyPhysics _physics;
    protected Health _health;
    protected EnemyInput _input;
    
    public bool isAlive { get => _health.IsAlive; }

    void Awake() {
        _health = GetComponent<Health>();
        _weapon = GetComponent<EnemyEquipment>();
        _input = GetComponent<EnemyInput>();
        _physics = GetComponent<EnemyPhysics>();
    }

    void Update() {
        _physics.SetPhysicsInput(_input.linearInput, _input.angularInput);
        
        _weapon.SetShooting(_input.isShooting);

        if (_input.targetDistance >= despawnDistance) {
            PoolManager.instance.Despawn(gameObject);
        }
    }

    void OnCollisionEnter(Collision col) {
        var h = col.collider.GetComponent<Health>();
        if (h) {
            h.TakeDamage(realDamage, shieldsDamage);
            _health.TakeDamage(realDamage, shieldsDamage);
        }
    }

    public void OnDie() {
        PoolManager.instance.Despawn(gameObject);
    }
}
