using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Canvas canv;
    
    void Awake() {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        Util.menu = this;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            LoadScene(1);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        canv.enabled = (scene.buildIndex == 1);
    }

    public void LoadScene(int i) 
    {
        SceneManager.LoadScene(i);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
