using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    protected bool _cooldownExpired = true;

    protected Transform _transform;

    public bool isShooting {get; protected set;} = false;

    protected virtual void Awake() {
        _transform = GetComponent<Transform>();
    }

    void OnDisable() {
        _cooldownExpired = true;
    }

    void LateUpdate() {
        if (isShooting && _cooldownExpired) {
            Shoot();
        }
    }
    
    public virtual void SetShooting(bool shooting) {
        isShooting = shooting;
    }

    protected virtual float cooldown { get; }

    protected void Shoot() {
        InstanciateShoot();
        _cooldownExpired = false;
        StartCoroutine(CooldownCoroutine());
    }

    protected IEnumerator CooldownCoroutine() {
        yield return new WaitForSeconds(cooldown);
        _cooldownExpired = true;
    }
    
    protected abstract void InstanciateShoot();
    
}
