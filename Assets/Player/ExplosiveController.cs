using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveController : MonoBehaviour
{
    public Lifecycle lifecycle;
    public GameObject explosionPrefab;

    void Update() {
        if (Input.GetKey(KeyCode.Q)) {
            Explode();
        }
    }

    public void Explode() {
        lifecycle.clones.Store(GetComponent<TrajectoryRecorder>().GetTrajectory());
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        lifecycle.RespawnLater();
        Destroy(gameObject);
    }

}
