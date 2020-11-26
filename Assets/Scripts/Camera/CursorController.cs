using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {
    public Image mouse;
    public Image crosshair;
    
    protected Transform _player;
    protected Camera _camera;
    protected RaycastHit _hit;

    void Awake () {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        _player = Player.instance.GetComponent<Transform>();
        _camera = GetComponent<Camera>();
    }

    void Update () {
        mouse.transform.position = Input.mousePosition;
        
        if(Physics.Raycast(_player.position, _player.forward, out _hit, Mathf.Infinity)){
            crosshair.transform.position = _camera.WorldToScreenPoint(_hit.point);
        } else {
            crosshair.transform.position = new Vector3 (Screen.width * .5f, Screen.height * .5f, .0f);
        }
    }
}
