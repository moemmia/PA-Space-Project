using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager> {

    protected Dictionary<string, List<GameObject>> _pool;
    protected Transform _poolParent;

    void Awake() {
        _poolParent = new GameObject("Pool").transform;
        _pool = new Dictionary<string, List<GameObject>>();
    }

    // Precarga del objeto
    public void Load(GameObject prefab, int amount=1){

        if (!_pool.ContainsKey(prefab.name)) {
            _pool[prefab.name] = new List<GameObject>();
        }

        for (int i = 0; i < amount; i++) {
            var go = Instantiate(prefab);
            go.name = prefab.name;
            go.SetActive(false);
            go.transform.parent = _poolParent;
            _pool[prefab.name].Add(go);
        }
    }

    public GameObject Spawn(GameObject prefab) {
        return Spawn(prefab, prefab.transform.position, prefab.transform.rotation);
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot) {
        return Spawn(prefab, pos, rot, prefab.transform.localScale);
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot, Vector3 scale) {
        if (!_pool.ContainsKey(prefab.name) || _pool[prefab.name].Count == 0) {
            Load(prefab);
        }
        var go = _pool[prefab.name][0];
        _pool[prefab.name].RemoveAt(0);
        go.transform.parent = null;
        var t = go.GetComponent<Transform>();
        t.position = pos;
        t.rotation = rot;
        t.localScale = scale;
        go.SetActive(true);
        return go;
    }

    public void Despawn(GameObject go) {
        go.SetActive(false);
        go.transform.parent = _poolParent;
        _pool[go.name].Add(go);
    }

}