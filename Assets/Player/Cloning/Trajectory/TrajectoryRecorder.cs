using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRecorder : MonoBehaviour
{
    private Trajectory trajectory = new Trajectory();
    public float currentRoundBeginning { get; private set; }

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
