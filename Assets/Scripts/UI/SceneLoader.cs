using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public KeyCode key;
    public string sceneName;
    void Update() {
        if(Input.GetKeyDown(key)) {
            SceneManager.LoadScene(sceneName);
        }
    }
}