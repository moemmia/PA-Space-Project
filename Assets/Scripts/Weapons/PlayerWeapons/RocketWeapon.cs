using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWeapon : PlayerWeapon {

    public GameObject rocketPrefab;
    protected override float cooldown {get => .5f;}

    protected override void InstanciateShoot() {
        RaycastHit hit;
        Physics.Raycast(_transform.position, _transform.forward, out hit, Mathf.Infinity);
        GameObject rocket = PoolManager.instance.Spawn(rocketPrefab, _transform.position + _transform.forward * 5f - _transform.up * 2f, _transform.rotation);
        rocket.GetComponent<Rocket>().SetObjective(hit.transform);
    }
}
