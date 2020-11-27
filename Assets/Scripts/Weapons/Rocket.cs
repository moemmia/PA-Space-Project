using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 15;
    public float turn = 15;
    public float timeToDespawn = 5;
    public int damage = 1;

    protected Transform _transform;
    protected Rigidbody _rg;
    protected Transform _objective;

    void Awake() {
        _transform = GetComponent<Transform>();
        _rg = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        StartCoroutine(DespawnCoroutine());
        _objective = null;
    }

    void Update(){
        if(_objective != null && !_objective.gameObject.activeSelf) {
            _objective = null;
        }
    }


    void FixedUpdate(){
        _rg.velocity = _transform.forward * speed;
        if(_objective != null) {
            Quaternion targetRotation = Quaternion.LookRotation(_objective.position - _transform.position);
            _rg.MoveRotation(Quaternion.RotateTowards(_transform.rotation, targetRotation,turn));
        }
    }

    public void SetObjective(Transform objective){
        _objective = objective;
    }

    private void OnDisable() {
        _rg.velocity = Vector3.zero;
        _rg.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision col) {
        // var h = col.collider.GetComponent<Health>();
        // if (h) {
        //     h.TakeDamage(damage);
        // }
        PoolManager.instance.Despawn(gameObject);
    }

    protected IEnumerator DespawnCoroutine() {
        yield return new WaitForSeconds(timeToDespawn);
        PoolManager.instance.Despawn(gameObject);
    }

}
