using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    protected List<SpawnableSettings> generateObjects;

    protected Transform _target;

    void Awake() {
        _target = Player.instance.GetComponent<Transform>();
    }

    void Start() {
        foreach (var obj in generateObjects) {
            PoolManager.instance.Load(obj.prefab, obj.maxNumber);
            for (int i =  0; i < obj.maxNumber; i++) {
                Generate(obj);
            }
        }
    }
    
    protected void Generate(SpawnableSettings obj){
        Vector3 randomDir = Random.insideUnitSphere.normalized;
        Vector3 spawnPos = _target.position + randomDir * Random.Range(obj.minDistance, obj.maxDistance);
        Quaternion spawnRot = Random.rotation;
        Vector3 spawnScale = Vector3.one * Random.Range(obj.minScale, obj.maxScale);
        PoolManager.instance.Spawn(obj.prefab, spawnPos, spawnRot, spawnScale);
    }

    public void DespawnAndRegerate(GameObject despawnable) {
        PoolManager.instance.Despawn(despawnable);
        Debug.Log(despawnable);
        SpawnableSettings obj = generateObjects.Find( x => x.prefab.name == despawnable.name);
        Debug.Log(obj);
        StartCoroutine(GenerateOnTime(obj));
    }

    protected IEnumerator GenerateOnTime(SpawnableSettings obj){
        Generate(obj);
        yield return new WaitForFixedUpdate();
    }

}
