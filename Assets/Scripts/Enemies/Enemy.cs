using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPhysics))]
[RequireComponent(typeof(EnemyInput))]
[RequireComponent(typeof(EnemyEquipment))]
public class Enemy : EnemyBase {

    protected EnemyEquipment _weapon;
    protected EnemyPhysics _physics;
    protected EnemyInput _input;
    
    protected override void Awake() {
        base.Awake();
        _weapon = GetComponent<EnemyEquipment>();
        _input = GetComponent<EnemyInput>();
        _physics = GetComponent<EnemyPhysics>();
    }

    protected override void Update() {
        base.Update();
        _physics.SetPhysicsInput(_input.linearInput, _input.angularInput);
        
        _weapon.SetShooting(_input.isShooting);
    }

}
