using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifecycle : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject explosionPrefab;
    public CameraFollow camera;

    ///TODO remove this, move Explode() into character
    private GameObject playerCharacter;

    void Start() {
        Respawn();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Explode();
        }
    }

    void Explode() {
        clones.Store(playerCharacter.GetComponent<Trajectory>());
        Instantiate(explosionPrefab, playerCharacter.transform.position, Quaternion.identity);
        Destroy(playerCharacter);
        Invoke("Respawn", 2f);
    }

    void Respawn() {
        clones.KillAllClones();
        clones.InstantiateClones();
        playerCharacter = Instantiate(characterPrefab, transform.position, transform.rotation, transform);
        playerCharacter.GetComponent<PlayerMovement>().directionProvider = camera.transform;
        camera.target = playerCharacter.transform;
    }

    CloneManager clones {
        get {
            return GameObject.Find("Managers").GetComponent<CloneManager>();
        }
    }

}
