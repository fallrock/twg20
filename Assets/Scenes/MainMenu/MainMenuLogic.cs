using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public void LoadGame() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        SceneManager.LoadScene(nextScene);
    }

    public void Quit() {
        Application.Quit();
    }
}
