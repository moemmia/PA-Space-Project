using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerEquipment))]
public class Player : Singleton<Player> {
    private PlayerInput _input;
    private PlayerPhysics _physics;  
    private PlayerEquipment _weapon;
    private Health _health;

    public HealthBar _healthBar;

    public bool isAlive { get => _health.IsAlive; }

    void Awake() {
        _input = GetComponent<PlayerInput>();
        _physics = GetComponent<PlayerPhysics>();
        _weapon = GetComponent<PlayerEquipment>();
        _health = GetComponent<Health>();
        _healthBar = GetComponent<HealthBar>();
        _healthBar.SetMaxHealth(_health.maxHealth);
        _healthBar.SetMaxShield(_health.maxShield);
    }

    void Update() {
        if(isAlive) {
            _physics.SetPhysicsInput(_input.linearInput, _input.angularInput);
            _weapon.SetShooting(_input.isShooting);
            _weapon.CicleSelectedWeapon(Mathf.RoundToInt(_input.scroll));
        }
        _healthBar.SetHealth(_health.GetHealth());
        _healthBar.SetShield(_health.GetShield());
    }

    public void OnDie() {
        
    }
    
}