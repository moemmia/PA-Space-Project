using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : PlayerWeapon {
    

    protected LineRenderer _lineRenderer;
    public float maxRayLength = 1000;

    protected override void Awake() {
        base.Awake();
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.widthMultiplier = 0.2f;
        _lineRenderer.positionCount = 2;
        _lineRenderer.startColor = _lineRenderer.endColor = Color.red;
    }
    
    protected override float cooldown {get => .0f;}

    protected override void InstanciateShoot() {
        _lineRenderer.SetPosition(0, _transform.position);
        RaycastHit hit;
        Vector3 shootDirection = _transform.forward;
        if(Physics.Raycast(_transform.position, shootDirection, out hit, maxRayLength)){
            _lineRenderer.SetPosition(1, hit.point);
        } else {
            _lineRenderer.SetPosition(1, _transform.position + shootDirection * maxRayLength);
        }
    }

    public override void SetShooting(bool isShooting) {
        base.SetShooting(isShooting);
        _lineRenderer.enabled = isShooting;
    }
}
