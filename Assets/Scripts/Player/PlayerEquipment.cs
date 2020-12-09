using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour {

    [SerializeField]
    protected Image weaponImage;

    [SerializeField]
    protected List<PlayerWeapon> weapons;

    protected int _selectedWeapon = 0;

    public void SetShooting(bool shooting) {
        weapons[_selectedWeapon].SetShooting(shooting);
    }

    public void CicleSelectedWeapon(int selectedWeapon) {
        int selected =  Mathf.Clamp(_selectedWeapon + selectedWeapon, 0, weapons.Count - 1);
        SetShooting(weapons[_selectedWeapon].isShooting && _selectedWeapon == selected);
        _selectedWeapon = selected;

        weaponImage.sprite = weapons[_selectedWeapon].GetWeaponSprite();
    }
    
}
