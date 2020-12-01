using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    protected float _pitch, _yaw, _strafe, _throttle;

    public Vector3 linearInput { get => new Vector3(_strafe, 0.0f, _throttle);}
    public Vector3 angularInput { get => new Vector3(_pitch, _yaw, 0.0f);}

    public bool isShooting {get; protected set;}
    public float scroll {get; protected set;}

    void Update() {
        CalcLinearInput();
        CalcAngularInput();

        MouseInput();
    }

    private void CalcLinearInput() {
        _strafe = Input.GetAxis("Horizontal");

        _throttle = Mathf.MoveTowards(_throttle, Input.GetAxis("Vertical"), Time.deltaTime * 0.5f);
    }

    private void CalcAngularInput() {
        Vector3 mousePosition = Input.mousePosition;
  
        _pitch = (mousePosition.y - (Screen.height * 0.5f)) / (Screen.height* 0.5f);
        _yaw = (mousePosition.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        _pitch = -Mathf.Clamp(_pitch, -1.0f, 1.0f);
        _yaw = Mathf.Clamp(_yaw, -1.0f, 1.0f);
    }

    private void MouseInput() {
        isShooting = Input.GetAxis("Fire1") > 0;
        scroll = Input.GetAxis("Mouse ScrollWheel") * 10;
    }

}
