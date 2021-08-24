using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRecorder : MonoBehaviour
{
    private Trajectory trajectory;
    private float currentRoundBeginning;

    public void ResetRound() {
        currentRoundBeginning = Time.time;
    }

    void Start()
    {
        trajectory = GetComponent<Trajectory>();
        currentRoundBeginning = Time.time;
    }

    void FixedUpdate()
    {
        trajectory.Put(new Trajectory.Point(
                           Time.time - currentRoundBeginning,
                           transform.position));
    }

}
