using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject indicatorPrefab;

    void Start() {
        GameObject indicator =
            GameObject.Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
        indicator.transform.parent = gameObject.transform;
        indicator
            .GetComponent<Indicator>()
            .Initialize(GetComponent<TrajectoryReproducer>().EndTime);
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
