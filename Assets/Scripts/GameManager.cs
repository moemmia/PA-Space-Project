using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [Header("Asteroid generation settings:")]
    public GameObject asteroidPrefab;
    public int maxAsteroidsNumber;
    public float minAsteroidDistance, maxAsteroidDistance, minAsteroidScale, maxAsteroidScale;
   
   
    protected Transform _playerTransform;

    void Awake() {
        _playerTransform = Player.instance.GetComponent<Transform>();
    }

    private void Start() {
        PoolManager.instance.Load(asteroidPrefab, maxAsteroidsNumber);
        StartCoroutine(GenerateAsteroidOnUpdate());
    }

    private IEnumerator GenerateAsteroidOnUpdate() {
        bool onAwake = false;
        System.Func<bool> checkAvailability = () => PoolManager.instance.FindNumberOfInstancesOf(asteroidPrefab) < maxAsteroidsNumber;
        while (Player.instance.isAlive) {
            onAwake = !checkAvailability();
            yield return new WaitUntil( checkAvailability );
            for (int i = 0; i < maxAsteroidsNumber/10; i++)
            {
                GenerateAsteroid(onAwake? maxAsteroidDistance - minAsteroidDistance : minAsteroidDistance, maxAsteroidDistance);
            }
        }
    }
    
    private void GenerateAsteroid(float minDistance, float maxDistance){
        Vector3 randomDir = Random.insideUnitSphere.normalized;
        Vector3 spawnPos = _playerTransform.position + randomDir * Random.Range(minDistance, maxDistance);
        Quaternion spawnRot = Random.rotation;
        Vector3 spawnScale = Vector3.one * Random.Range(minAsteroidScale, maxAsteroidScale);
        PoolManager.instance.Spawn(asteroidPrefab, spawnPos, spawnRot, spawnScale);
    }
}
