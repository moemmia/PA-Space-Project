using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyPhysics : MonoBehaviour {
    
    protected Rigidbody _rg;
    protected Vector3 _appliedLinearForce = Vector3.zero;
    protected Quaternion _appliedAngularMove = Quaternion.identity;

    void Awake() {
        _rg = GetComponent<Rigidbody>();
    }

    void OnDisable() {
        _rg.velocity = Vector3.zero;
        _rg.angularVelocity = Vector3.zero;
    }

    void FixedUpdate() {
        _rg.MoveRotation(_appliedAngularMove);
        _rg.AddForce(_appliedLinearForce, ForceMode.Acceleration);
    }

    public void SetPhysicsInput(Vector3 linearInput, Quaternion angularInput) {
        _appliedLinearForce = linearInput;
        _appliedAngularMove = angularInput;
    }

}
