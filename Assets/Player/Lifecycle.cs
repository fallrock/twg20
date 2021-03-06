using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifecycle : MonoBehaviour
{
    public float respawnTime = 2f;

    public GameObject characterPrefab;
    public CameraFollow playerCamera;

    public CloneManager clones
    {
        get { return GetComponent<CloneManager>(); }
    }

    void Start() {
        RespawnImmediately();
    }

    void Update() {
    }

    public void RespawnLater() {
        Invoke("RespawnImmediately", respawnTime);
    }

    public void RespawnImmediately() {
        clones.KillAllClones();
        clones.InstantiateClones();
        var character = Instantiate(this.characterPrefab, transform.position, transform.rotation, transform);
        character.GetComponent<PlayerMovement>().directionProvider = this.playerCamera.transform;
        character.GetComponent<ExplosiveController>().lifecycle = this;
        GetComponent<WinLogic>().playerMovement = character.GetComponent<PlayerMovement>();
        GetComponent<WinLogic>().cameraControls = this.playerCamera.GetComponent<CameraControls>();
        GetComponent<Hud>().roundStart = Time.time;
        this.playerCamera.target = character.transform;
    }

}
