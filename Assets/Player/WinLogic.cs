using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLogic : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public CameraControls cameraControls;
    public PlayerMovement playerMovement;

    public bool won { get; private set; } = false;

    public void Win() {
        levelCompleteUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        cameraControls.enabled = false;
        playerMovement.enabled = false;
    }

}
