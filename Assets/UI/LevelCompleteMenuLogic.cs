using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenuLogic : MonoBehaviour
{
    private int currentScene { get { return SceneManager.GetActiveScene().buildIndex; } }

    public void NextLevel() {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void Restart() {
        SceneManager.LoadScene(currentScene);
    }

    public void QuitToMain() {
        SceneManager.LoadScene(0);
    }
}
