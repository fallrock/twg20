using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public void LoadGame(int i) {
        SceneManager.LoadScene(i);
    }

    public void Quit() {
        Application.Quit();
    }
}
