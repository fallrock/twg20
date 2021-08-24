using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifecycle : MonoBehaviour
{
    public GameObject playerCharacter;
    public Transform playerSpawn;
    public GameObject explosionPrefab;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Explode();
        }
    }

    void Explode() {
        clones.Store(playerCharacter.GetComponent<Trajectory>());
        Instantiate(explosionPrefab, playerCharacter.transform.position, Quaternion.identity);
        playerCharacter.SetActive(false);
        Invoke("Respawn", 2f);
    }

    void Respawn() {
        clones.InstantiateClones();
        playerCharacter.transform.position = playerSpawn.position;
        playerCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerCharacter.GetComponent<Trajectory>().Reset();
        playerCharacter.GetComponent<TrajectoryRecorder>().ResetRound();
        playerCharacter.SetActive(true);
    }

    CloneManager clones {
        get {
            return GameObject.Find("Managers").GetComponent<CloneManager>();
        }
    }

}
