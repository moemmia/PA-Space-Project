using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWeapon : PlayerWeapon {

    
    protected override float cooldown {get => .5f;}

    protected override void InstanciateShoot() { 

    }
}
