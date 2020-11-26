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

    public bool isAlive { get => true; }

    void Awake() {
        _input = GetComponent<PlayerInput>();
        _physics = GetComponent<PlayerPhysics>();
        _weapon = GetComponent<PlayerEquipment>();
    }

    void Update() {
        _physics.SetPhysicsInput(_input.linearInput, _input.angularInput);
        _weapon.SetShooting(_input.isShooting);
    }
    
}