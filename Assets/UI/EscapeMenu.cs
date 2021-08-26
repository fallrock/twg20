using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public GameObject ui;

    private CursorLockMode savedMode;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (ui.activeSelf) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void QuitToMain() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Pause() {
        ui.SetActive(true);
        Time.timeScale = 0f;
        savedMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume() {
        ui.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = savedMode;
    }
}
