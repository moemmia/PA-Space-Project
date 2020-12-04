using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : BaseCursorController {

    [SerializeField]
    public Image crosshairImage;
    
    protected Transform _target;
    protected Camera _camera;

    protected override void Awake () {
        base.Awake();
        _target = Player.instance.GetComponent<Transform>();
        _camera = GetComponent<Camera>();
    }

    protected override void Update () {
        base.Update();
        if(Physics.Raycast(_target.position, _target.forward, out _hit, Mathf.Infinity)){
            crosshairImage.transform.position = _camera.WorldToScreenPoint(_hit.point);
        } else {
            crosshairImage.transform.position = new Vector3 (Screen.width * .5f, Screen.height * .5f, .0f);
        }
    }
    
}
