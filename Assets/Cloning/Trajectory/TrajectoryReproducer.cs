using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryReproducer : MonoBehaviour
{
    private Trajectory trajectory;

    void Start()
    {
        trajectory = GetComponent<Trajectory>();
    }

    void FixedUpdate()
    {
        if (trajectory.trajectory.Count == 0)
        {
            // Explode();
        }

        float localRoundTime = Time.time - currentRoundBeginning;
        var currentPosition =
            trajectory.FindLast(x => x.Item1 < localRoundTime);
        transform.position = currentPosition.Item2;
        if (currentPosition == trajectory[trajectory.Count - 1]) {
            // Explode();
        }

    }

    public void StartRound(float time) {
        currentRoundBeginning = Time.time;
    }
    public void StartRound() {
        StartRound(Time.time);
    }

    // [HideInInspector]
    private float currentRoundBeginning;
}
