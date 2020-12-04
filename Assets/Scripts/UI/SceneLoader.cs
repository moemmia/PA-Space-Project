using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [SerializeField]
    protected KeyCode key;

    [SerializeField]
    protected string sceneName;

    void Update() {
        if(Input.GetKeyDown(key)) {
            LoadScene();
        }
    }

    public void LoadScene() {
        SceneManager.LoadScene(sceneName);
    }
    
}