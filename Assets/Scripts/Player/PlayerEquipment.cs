using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerEquipment : MonoBehaviour {

    public List<PlayerWeapon> weapons;
    private int _selectedWeapon;

    public void SetShooting(bool isShooting) {
        weapons[_selectedWeapon].SetShooting(isShooting);
    }

    public void SetSelectedWeapon(int selectedWeapon) {
        if(weapons.Count < selectedWeapon) {
            _selectedWeapon = selectedWeapon;
        }
    }
}
