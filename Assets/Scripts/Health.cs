using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 1;
    public float maxShield = 1;
    public float regenerateShieldRatio = 1;

    public float immortalTimer = .75f;
    public bool IsAlive {
        get {
            return _currentHealth > 0;
        }
    }
    protected float _currentHealth = 1;
    protected float _currentShield = 1;
    protected Renderer _r;
    protected bool _immortal;

    protected Color _startingColor;

    public UnityEvent Die;

    public float GetHealth(){
        return _currentHealth;
    }

    public float GetShield(){
        return _currentShield;
    }

    private void Awake() {
        _r = GetComponent<Renderer>();
        _startingColor = _r.material.GetColor("_EmissionColor");
    }

    void OnEnable() {
        _currentHealth = maxHealth;
        _currentShield = maxShield;
        _r.material.SetColor("_EmissionColor", _startingColor);
        StartCoroutine(RegenerateShield());
    }

    public void TakeDamage(float realDamage,float shieldsDamage) {
        if (!_immortal) {
            _currentShield -= shieldsDamage;
            if(_currentShield <= 0) {
                    _currentHealth -= realDamage;
                if (_currentHealth > 0)
                {
                    StartCoroutine(DamageFlash(Color.red));
                } else {
                    Die.Invoke();
                }
            } else {
                 StartCoroutine(DamageFlash(Color.white));
            }
        }
    }

    protected IEnumerator RegenerateShield() {
        while(IsAlive) {
            if(_currentShield < maxShield) {
                yield return new WaitForSeconds(1f);
                _currentShield += regenerateShieldRatio;
            }
            yield return new WaitForEndOfFrame();
        }
        
    }

    protected IEnumerator DamageFlash(Color col) {
        for (int i = 0; i < 5; i++) {
            _r.material.SetColor("_EmissionColor", i % 2 == 0 ? col : _startingColor);
            yield return new WaitForSeconds(.1f);
        }
        _r.material.SetColor("_EmissionColor", Color.Lerp(col, _startingColor, _currentHealth / maxHealth));
    }

    protected IEnumerator ImmortalTimer()
    {
        _immortal = true;
        yield return new WaitForSeconds(immortalTimer);
        _immortal = false;
    }

}
