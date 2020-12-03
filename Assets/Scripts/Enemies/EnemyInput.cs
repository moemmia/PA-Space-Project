using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    public float linearForce = 1000f;
    public float angularForce = 1f;
    protected Transform _transform;
    protected Transform _target;
    public float maxDistanceToTarget = 10f;
    public float minDistanceShootTarget = 100f;
    public float maxAngleShootTarget = 25f;

    public bool isShooting {get; protected set;}
    public Vector3 linearInput { get; protected set;}
    public Quaternion angularInput { get; protected set;} = Quaternion.identity;

    public Vector3 targetDirection {get  => _target.position - _transform.position;}
    public float targetAngle {get  => Vector3.Angle(_transform.forward, targetDirection);}
    public float targetDistance {get  => Vector3.Distance(_transform.position,  _target.position) - maxDistanceToTarget;}
    

    void Awake() {
        _transform = GetComponent<Transform>();
        _target = Player.instance.GetComponent<Transform>();
    }

    void OnEnable() {
        maxDistanceToTarget += _transform.localScale.x * 2;
    }

    void OnDisable() {
        maxDistanceToTarget -= _transform.localScale.x * 2;
    }

    void Update() {
        CalcLinearInput();
        CalcAngularInput();
        ShootInput();
    }

    private void CalcLinearInput() {
        linearInput = _transform.forward * linearForce * targetDistance;
    }

    private void CalcAngularInput() {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        angularInput = Quaternion.RotateTowards(_transform.rotation, targetRotation, angularForce);
    }

    private void ShootInput() {
        isShooting = targetDistance <= minDistanceShootTarget && targetAngle < maxAngleShootTarget;
    }
}
