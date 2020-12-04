using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysics : MonoBehaviour {

    [SerializeField]
    protected float linearForce = 50.0f;

    [SerializeField]
    protected float angularForce = 10.0f;


    protected Rigidbody _rg;
    
    protected Vector3 _appliedLinearForce = Vector3.zero;
    protected Vector3 _appliedAngularForce = Vector3.zero;

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
