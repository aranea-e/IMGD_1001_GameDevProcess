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
        if (Input.GetKeyDown("escape") || PlayerInputs.menu) {
            Audio.updateMusic("menu");
            LoadScene(1, true);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        canv.enabled = (scene.buildIndex == 1);
    }

    public void ButtonPress(int i) {
        LoadScene(i, true);
    }
    public void LoadScene(int i, bool audio) 
    {
        if (audio) {
            Audio.play("select");
        }
        if (i == 2) {
            Audio.updateMusic("game");
        }
        if (i == 5)
        {
            Audio.updateMusic("");
        }
        SceneManager.LoadScene(i);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
