using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRecorder : MonoBehaviour
{
    private Trajectory trajectory;

    void Start()
    {
        trajectory = GetComponent<Trajectory>();
    }

    void FixedUpdate()
    {
        trajectory.Put(transform.position);
    }
}
