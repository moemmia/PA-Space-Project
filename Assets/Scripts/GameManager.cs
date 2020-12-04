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
            StartCoroutine(GenerateOnUpdate(obj.prefab, obj.maxNumber, obj.maxDistance, obj.minDistance, obj.maxScale, obj.minScale));
        }
    }

    protected IEnumerator GenerateOnUpdate(GameObject prefab, int maxNumber, float maxDistance, float minDistance, float maxScale, float minScale) {
        bool onAwake = false;
        System.Func<bool> checkAvailability = () => PoolManager.instance.FindNumberOfInstancesOf(prefab) < maxNumber;
        while (Player.instance.isAlive) {
            onAwake = !checkAvailability();
            yield return new WaitUntil( checkAvailability );
            for (int i =  PoolManager.instance.FindNumberOfInstancesOf(prefab); i < maxNumber; i++)
            {
                Generate(prefab, onAwake? maxDistance - minDistance : minDistance, maxDistance, maxScale, minScale );
            }
        }
    }
    
    protected void Generate(GameObject prefab, float minDistance, float maxDistance, float maxScale, float minScale){
        Vector3 randomDir = Random.insideUnitSphere.normalized;
        Vector3 spawnPos = _target.position + randomDir * Random.Range(minDistance, maxDistance);
        Quaternion spawnRot = Random.rotation;
        Vector3 spawnScale = Vector3.one * Random.Range(minScale, maxScale);
        PoolManager.instance.Spawn(prefab, spawnPos, spawnRot, spawnScale);
    }

}
