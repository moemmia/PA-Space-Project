using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : Weapon {
    
    [SerializeField]
    protected Sprite weaponSprite;

    public Sprite GetWeaponSprite() {
        return weaponSprite;
    }

}