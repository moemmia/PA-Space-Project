using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    protected Vector3 offset = Vector3.zero;
    
    [SerializeField]
    protected float smooth = 1;

    protected Transform _transform;
    protected Transform _target;
    
    void Awake() {
        _transform = GetComponent<Transform>();
        _target = Player.instance.GetComponent<Transform>();
    }

    void FixedUpdate() {
        _transform.position = _target.TransformPoint(offset);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, _target.rotation, smooth * Time.deltaTime);
    }

} 
