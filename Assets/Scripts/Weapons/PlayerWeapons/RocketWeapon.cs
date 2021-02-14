using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWeapon : PlayerWeapon {

    [SerializeField]
    protected GameObject rocketPrefab;
    protected Transform _camera;

    protected RaycastHit _hit;
    protected override float cooldown {get => .5f;}

    void Start(){
        PoolManager.instance.Load(rocketPrefab, 20);
        _camera = Camera.main.transform;
    }

    protected override void InstanciateShoot() {
        Physics.Raycast(_camera.position, _camera.forward, out _hit, Mathf.Infinity);
        GameObject rocket = PoolManager.instance.Spawn(rocketPrefab, _transform.position + _transform.forward * 5f - _transform.up * 2f, _transform.rotation);
        rocket.GetComponent<Rocket>().SetObjective(_hit.transform);
    }

}
