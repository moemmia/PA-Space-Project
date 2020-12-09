using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    [SerializeField]
    protected float maxHealth = 1;

    [SerializeField]
    protected float maxShield = 1;

    [SerializeField]
    protected float regenerateShieldRatio = 1;

    [SerializeField]
    protected float immortalTimer = .75f;

    public bool IsAlive {
        get {
            return currentHealth > 0;
        }
    }

    public float currentHealth {get; protected set;} = 1;
    public float currentShield {get; protected set;} = 1;
    protected Renderer _r;
    protected bool _immortal;

    protected Color _startingColor;
    protected Color _currentColor;

    [SerializeField]
    protected UnityEvent Die;

    private void Awake() {
        _r = GetComponent<Renderer>();
        _startingColor = _r.material.GetColor("_EmissionColor");
    }

    void OnEnable() {
        currentHealth = maxHealth;
        currentShield = maxShield;
        _r.material.SetColor("_EmissionColor", _startingColor);
        _currentColor = _startingColor;
        StartCoroutine(RegenerateShield());
    }

    public float GetMaxHealth(){
        return maxHealth;
    }

    public float GetMaxShield(){
        return maxShield;
    }

    public void TakeDamage(float realDamage,float shieldsDamage) {
        if (!_immortal) {
            currentShield -= shieldsDamage;
            if(currentShield <= 0) {
                    currentHealth -= realDamage;
                if (currentHealth > 0)
                {
                    StartCoroutine(DamageFlash(Color.red, currentHealth/maxHealth));
                } else {
                    Die.Invoke();
                }
            } else {
                 StartCoroutine(DamageFlash(Color.white, 1));
            }
        }
    }

    protected IEnumerator RegenerateShield() {
        while(IsAlive) {
            if(currentShield < maxShield) {
                yield return new WaitForSeconds(1f);
                currentShield += regenerateShieldRatio;
            }
            yield return new WaitForEndOfFrame();
        }
        
    }

    protected IEnumerator DamageFlash(Color col, float transition) {
        for (int i = 0; i < 5; i++) {
            _r.material.SetColor("_EmissionColor", i % 2 == 0 ? col : _currentColor);
            yield return new WaitForSeconds(.1f);
        }
        _currentColor = Color.Lerp(col, _currentColor, transition);
        _r.material.SetColor("_EmissionColor", _currentColor);
    }

    protected IEnumerator ImmortalTimer()
    {
        _immortal = true;
        yield return new WaitForSeconds(immortalTimer);
        _immortal = false;
    }

}
