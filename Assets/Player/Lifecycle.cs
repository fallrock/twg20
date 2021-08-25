using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifecycle : MonoBehaviour
{
    public float respawnTime = 2f;

    public GameObject characterPrefab;
    public CameraFollow playerCamera;

    void Start() {
        RespawnImmediately();
    }

    void Update() {
    }

    public void RespawnLater() {
        Invoke("RespawnImmediately", respawnTime);
    }

    public void RespawnImmediately() {
        var clones = GameObject.Find("Managers").GetComponent<CloneManager>();
        clones.KillAllClones();
        clones.InstantiateClones();
        var character = Instantiate(this.characterPrefab, transform.position, transform.rotation, transform);
        character.GetComponent<PlayerMovement>().directionProvider = this.playerCamera.transform;
        character.GetComponent<ExplosiveController>().lifecycle = this;
        this.playerCamera.target = character.transform;
    }

}
