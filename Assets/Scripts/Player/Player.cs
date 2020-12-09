using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthBar))]
[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerEquipment))]
public class Player : Singleton<Player> {
    
    protected PlayerInput _input;
    protected PlayerPhysics _physics;  
    protected PlayerEquipment _weapon;
    protected Health _health;
    protected HealthBar _healthBar;

    public bool isAlive { get => _health.IsAlive; }

    void Awake() {
        _input = GetComponent<PlayerInput>();
        _physics = GetComponent<PlayerPhysics>();
        _weapon = GetComponent<PlayerEquipment>();
        _health = GetComponent<Health>();
        _healthBar = GetComponent<HealthBar>();
        _healthBar.SetMaxHealth(_health.GetMaxHealth());
        _healthBar.SetMaxShield(_health.GetMaxShield());
    }

    void Update() {
        if(isAlive) {
            _physics.SetPhysicsInput(_input.linearInput, _input.angularInput);
            _weapon.SetShooting(_input.isShooting);
            _weapon.CicleSelectedWeapon(Mathf.RoundToInt(_input.scroll));
        }
        _healthBar.SetHealth(_health.currentHealth);
        _healthBar.SetShield(_health.currentShield);
    }

    public void OnDie() {
        _weapon.SetShooting(false);
    }
    
}