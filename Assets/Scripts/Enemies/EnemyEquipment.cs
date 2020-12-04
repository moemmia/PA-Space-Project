using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEquipment : MonoBehaviour
{
    [SerializeField]
    protected EnemyWeapon weapon;
    
    public void SetShooting(bool isShooting) {
        weapon.SetShooting(isShooting);
    }
    
}
