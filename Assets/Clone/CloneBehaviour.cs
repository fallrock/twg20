using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;

    void FixedUpdate()
    {
        if (trajectory.Count == 0) return;
        float localRoundTime = Time.time - currentRoundBeginning;
        var currentPosition =
            trajectory.FindLast(x => x.Item1 < localRoundTime);
        transform.position = currentPosition.Item2;
        if (currentPosition == trajectory[trajectory.Count - 1]) {
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

    public void StartRound(float time) {
        currentRoundBeginning = Time.time;
    }
    public void StartRound() {
        StartRound(Time.time);
    }

    // [HideInInspector]
    private float currentRoundBeginning;

    public List<(float, Vector3)> trajectory
      = new List<(float, Vector3)>();

}
