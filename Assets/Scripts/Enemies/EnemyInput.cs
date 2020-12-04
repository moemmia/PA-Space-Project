using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour {

    [SerializeField]
    protected float linearForce = 1000f;

    [SerializeField]
    protected float angularForce = 1f;

    [SerializeField]
    protected float maxDistanceToTarget = 10f;

    [SerializeField]
    protected float minDistanceShootTarget = 100f;

    [SerializeField]
    protected float maxAngleShootTarget = 25f;

    protected Transform _transform;
    protected Transform _target;

    public bool isShooting {get; protected set;} = false;
    public Vector3 linearInput { get; protected set;} = Vector3.zero;
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

    protected void CalcLinearInput() {
        linearInput = _transform.forward * linearForce * targetDistance;
    }

    protected void CalcAngularInput() {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        angularInput = Quaternion.RotateTowards(_transform.rotation, targetRotation, angularForce);
    }

    protected void ShootInput() {
        isShooting = targetDistance <= minDistanceShootTarget && targetAngle < maxAngleShootTarget;
    }

}
