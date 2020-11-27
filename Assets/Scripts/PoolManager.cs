﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager> {
    Dictionary<string, List<GameObject>> pool, instanciatedPool;
    Transform poolParent;

    private void Awake() {
        poolParent = new GameObject("Pool").transform;
        instanciatedPool = new Dictionary<string, List<GameObject>>();
        pool = new Dictionary<string, List<GameObject>>();
    }

    // Precarga del objeto
    public void Load(GameObject prefab, int amount=1){

        if (!pool.ContainsKey(prefab.name)) {
            pool[prefab.name] = new List<GameObject>();
            instanciatedPool[prefab.name] = new List<GameObject>();
        }

        for (int i = 0; i < amount; i++) {
            var go = Instantiate(prefab);
            go.name = prefab.name;
            go.SetActive(false);
            go.transform.parent = poolParent;
            pool[prefab.name].Add(go);
        }
    }

    public GameObject Spawn(GameObject prefab) {
        return Spawn(prefab, prefab.transform.position, prefab.transform.rotation);
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot) {
        return Spawn(prefab, pos, rot, prefab.transform.localScale);
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot, Vector3 scale) {
        if (!pool.ContainsKey(prefab.name) || pool[prefab.name].Count == 0) {
            Load(prefab);
        }
        var go = pool[prefab.name][0];
        pool[prefab.name].RemoveAt(0);
        go.transform.parent = null;
        var t = go.GetComponent<Transform>();
        t.position = pos;
        t.rotation = rot;
        t.localScale = scale;
        go.SetActive(true);
        instanciatedPool[prefab.name].Add(go);
        return go;
    }

    public void Despawn(GameObject go) {
        instanciatedPool[go.name].Remove(go);
        go.SetActive(false);
        go.transform.parent = poolParent;
        pool[go.name].Add(go);
    }

    public int FindNumberOfInstancesOf(GameObject go) {
        return instanciatedPool[go.name].Count;
    }

}