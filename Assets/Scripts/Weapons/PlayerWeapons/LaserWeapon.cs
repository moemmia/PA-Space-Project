using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : PlayerWeapon {
    
    [SerializeField]
    protected float maxRayLength = 1000;

    [SerializeField]
    protected float realDamage = 1f;

    [SerializeField]
    protected float shieldsDamage = 5f;

    [SerializeField]
    protected float timeBetweenDamage = 0.1f;
    protected Transform _camera;
    
    protected LineRenderer _lineRenderer;
    protected RaycastHit _hit;

    protected Health _currentObj;
    protected float startTime;

    protected override float cooldown {get => .0f;}

    protected override void Awake() {
        base.Awake();
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.widthMultiplier = 0.2f;
        _lineRenderer.positionCount = 2;
        _lineRenderer.startColor = _lineRenderer.endColor = Color.red;
    }

    void Start(){
        _camera = Camera.main.transform;
    }

    public override void SetShooting(bool shooting) {
        base.SetShooting(shooting);
        _lineRenderer.enabled = shooting;
    }

    protected override void InstanciateShoot() {
        _lineRenderer.SetPosition(0, _transform.position);
        
        Vector3 shootDirection = _camera.forward;
        if(Physics.Raycast(_camera.position, shootDirection, out _hit, maxRayLength)){
            _lineRenderer.SetPosition(1, _hit.point);
            var h = _hit.collider.GetComponent<Health>();
            if (h) {
                if(h == _currentObj){
                    if(Time.time - startTime >= timeBetweenDamage) {
                        h.TakeDamage(realDamage, shieldsDamage);
                        startTime = Time.time;
                    }
                } else {
                    _currentObj = h;
                    startTime = Time.time;
                    h.TakeDamage(realDamage, shieldsDamage);
                }
            }
        } else {
            _lineRenderer.SetPosition(1, _transform.position + shootDirection * maxRayLength);
        }
    }
    
}
