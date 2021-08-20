using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryReproducer : MonoBehaviour
{
    private Trajectory trajectory;

    public bool finished { get; private set; } = false;
    private bool playing = false;
    private float currentRoundBeginning;

    void Start()
    {
        trajectory = GetComponent<Trajectory>();
    }

    void FixedUpdate()
    {
        if (!playing) return;

        if (trajectory.trajectory.Count == 0)
        {
            playing = false;
            finished = true;
        }

        float localRoundTime = Time.time - currentRoundBeginning;
        var currentPoint =
            trajectory.trajectory.FindLast(
                x => x.time < localRoundTime
            );

        ReproducePoint(currentPoint);

        if (currentPoint ==
            trajectory.trajectory[trajectory.trajectory.Count - 1])
        {
            playing = false;
            finished = true;
        }

    }

    private void ReproducePoint(Trajectory.Point point)
    {
        transform.position = point.position;
    }

    public void StartReproducing(float time) {
        currentRoundBeginning = Time.time;
        playing = true;
        finished = false;
    }

    public void StartReproducing() {
        StartReproducing(Time.time);
    }

}
