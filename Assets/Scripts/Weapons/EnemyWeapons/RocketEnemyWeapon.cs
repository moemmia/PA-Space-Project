using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEnemyWeapon : EnemyWeapon
{
    public GameObject rocketPrefab;
    protected override float cooldown {get => 2.5f;}

    void Start(){
        PoolManager.instance.Load(rocketPrefab, 20);
    }
    
    protected override void InstanciateShoot() {
        GameObject rocket = PoolManager.instance.Spawn(rocketPrefab, _transform.position + _transform.forward * (_transform.localScale.x + 5f), _transform.rotation);
    }
}
