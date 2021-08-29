using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenuLogic : MonoBehaviour
{
    private int currentScene { get { return SceneManager.GetActiveScene().buildIndex; } }

    public void NextLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitToMain() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
