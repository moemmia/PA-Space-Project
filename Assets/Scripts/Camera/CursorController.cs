using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : BaseCursorController {
    public Image crosshair;
    
    protected Transform _player;
    protected Camera _camera;

    protected override void Awake () {
        base.Awake();
        _player = Player.instance.GetComponent<Transform>();
        _camera = GetComponent<Camera>();
    }

    protected override void Update () {
        base.Update();
        
        if(Physics.Raycast(_player.position, _player.forward, out _hit, Mathf.Infinity)){
            crosshair.transform.position = _camera.WorldToScreenPoint(_hit.point);
        } else {
            crosshair.transform.position = new Vector3 (Screen.width * .5f, Screen.height * .5f, .0f);
        }
    }
}
