using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    protected Rigidbody _rg;
    
    public float linearForce = 50.0f;
    private Vector3 _appliedLinearForce = Vector3.zero;

    public float angularForce = 10.0f;
    private Vector3 _appliedAngularForce = Vector3.zero;

    void Awake() {
        _rg = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        _rg.AddRelativeForce(_appliedLinearForce, ForceMode.Acceleration);
        _rg.AddRelativeTorque(_appliedAngularForce, ForceMode.Acceleration);
    }

    public void SetPhysicsInput(Vector3 linearInput, Vector3 angularInput) {
        _appliedLinearForce = linearInput * linearForce;
        _appliedAngularForce = angularInput * angularForce;
    }

}
