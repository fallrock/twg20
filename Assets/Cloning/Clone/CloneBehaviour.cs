using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;

    void Start()
    {
        GetComponent<TrajectoryReproducer>().StartReproducing();
    }

    void FixedUpdate()
    {
        if (GetComponent<TrajectoryReproducer>().finished) {
            Explode();
        }
    }

    private void Explode() {
        GameObject.Instantiate(explosionPrefab,
                                transform.position,
                                new Quaternion()
        );
        GameObject.Destroy(gameObject);
    }

}
