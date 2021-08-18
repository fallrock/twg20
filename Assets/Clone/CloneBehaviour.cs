using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehaviour : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (trajectory.Count == 0) return;
        float currentRoundTime = Time.time - currentRoundBeginning;
        transform.position =
            trajectory.FindLast(x => x.Item1 < currentRoundTime)
            .Item2;
    }

    public void RestartRound(float time) {
        currentRoundBeginning = Time.time;
    }
    public void RestartRound() {
        RestartRound(Time.time);
    }

    // [HideInInspector]
    private float currentRoundBeginning;

    public List<(float, Vector3)> trajectory
      = new List<(float, Vector3)>();

}
