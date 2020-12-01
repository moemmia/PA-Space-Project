using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [Header("Asteroid generation settings:")]
    public GameObject asteroidPrefab;
    public int maxAsteroidsNumber;
    public float minAsteroidDistance, maxAsteroidDistance, minAsteroidScale, maxAsteroidScale;
   
   
    [Header("Shipping enemies generation settings:")]
    public GameObject enemyShipPrefab;
    public int maxEnemyShipNumber;
    public float minEnemyShipDistance, maxEnemyShipDistance, minEnemyShipScale, maxEnemyShipScale;
   
    protected Transform _playerTransform;

    void Awake() {
        _playerTransform = Player.instance.GetComponent<Transform>();
    }

    private void Start() {
        PoolManager.instance.Load(asteroidPrefab, maxAsteroidsNumber);
        PoolManager.instance.Load(enemyShipPrefab, maxEnemyShipNumber);
        StartCoroutine(GenerateOnUpdate(asteroidPrefab, maxAsteroidsNumber, maxAsteroidDistance, minAsteroidDistance, maxAsteroidScale, minAsteroidScale));
        StartCoroutine(GenerateOnUpdate(enemyShipPrefab, maxEnemyShipNumber, maxEnemyShipDistance, minEnemyShipDistance, maxEnemyShipScale, minEnemyShipScale));
    }

    private IEnumerator GenerateOnUpdate(GameObject prefab, int maxNumber, float maxDistance, float minDistance, float maxScale, float minScale) {
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
    
    private void Generate(GameObject prefab, float minDistance, float maxDistance, float maxScale, float minScale){
        Vector3 randomDir = Random.insideUnitSphere.normalized;
        Vector3 spawnPos = _playerTransform.position + randomDir * Random.Range(minDistance, maxDistance);
        Quaternion spawnRot = Random.rotation;
        Vector3 spawnScale = Vector3.one * Random.Range(minScale, maxScale);
        PoolManager.instance.Spawn(prefab, spawnPos, spawnRot, spawnScale);
    }

}
