using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trajectory = System.Collections.Generic.List<UnityEngine.Vector3>;

public class ExplosiveController : MonoBehaviour
{
    public Lifecycle lifecycle;
    public GameObject explosionPrefab;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Explode();
        }
    }

    void Explode() {
        var clones = GameObject.Find("Managers").GetComponent<CloneManager>();
        clones.Store(GetComponent<TrajectoryRecorder>().GetTrajectory());
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        lifecycle.RespawnLater();
        Destroy(gameObject);
    }

}
