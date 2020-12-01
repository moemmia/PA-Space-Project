using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : Weapon {
    
    [SerializeField]
    protected Sprite _weaponImage;

    public Sprite GetWeaponImage() {
        return _weaponImage;
    }

}
