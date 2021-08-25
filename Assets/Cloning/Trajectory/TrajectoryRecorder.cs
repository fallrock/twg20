using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRecorder : MonoBehaviour
{
    private Trajectory trajectory = new Trajectory();
    private float currentRoundBeginning;

    void Start() {
        Reset();
    }

    void FixedUpdate() {
        trajectory.Add(transform.position);
    }

    public Trajectory GetTrajectory() {
        return trajectory;
    }

    public void Reset() {
        trajectory.Clear();
        currentRoundBeginning = Time.time;
    }

}
