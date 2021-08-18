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
        var currentPosition =
            trajectory.FindLast(x => x.Item1 < currentRoundTime);
        transform.position = currentPosition.Item2;
        if (currentPosition == trajectory[trajectory.Count - 1]) {
            Explode();
        }
    }

    public void RestartRound(float time) {
        currentRoundBeginning = Time.time;
    }
    public void RestartRound() {
        RestartRound(Time.time);
    }

    private void Explode() {
        Debug.Log("*Explosion*");
        Destroy(gameObject);
    }

    // [HideInInspector]
    private float currentRoundBeginning;

    public List<(float, Vector3)> trajectory
      = new List<(float, Vector3)>();

}
