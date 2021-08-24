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

        Trajectory.Point? point =
            trajectory
            .GetClosestPoint(Time.time - currentRoundBeginning);

        if (point == null)
        {
            playing = false;
            finished = true;
            return;
        }

        ReproducePoint(point.Value);

        if (point ==
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
        currentRoundBeginning = time;
        playing = true;
        finished = false;
    }

    public void StartReproducing() {
        StartReproducing(Time.time);
    }

}
