using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    protected bool _cooldownExpired = true;

    protected Transform _transform;
    protected bool _isShooting = false;

    protected virtual void Awake() {
        _transform = GetComponent<Transform>();
    }

    private void OnDisable() {
        _cooldownExpired = true;
    }

    private void LateUpdate() {
        if (_isShooting && _cooldownExpired) {
            Shoot();
        }
    }

    protected void Shoot() {
        InstanciateShoot();
        _cooldownExpired = false;
        StartCoroutine(CooldownCoroutine());
    }

    protected IEnumerator CooldownCoroutine() {
        yield return new WaitForSeconds(cooldown);
        _cooldownExpired = true;
    }

    protected virtual float cooldown { get; }
    public virtual void SetShooting(bool isShooting) {
        _isShooting = isShooting;
    }

    protected abstract void InstanciateShoot();
    
}
