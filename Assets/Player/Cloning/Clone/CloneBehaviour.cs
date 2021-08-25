using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;

    void Start() {
    }

    void FixedUpdate() {
        if (GetComponent<TrajectoryReproducer>().finished) {
            Explode();
        }
    }

    private void Explode() {
        GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }

}
