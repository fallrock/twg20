using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneReproductionSystem : MonoBehaviour
{
    void Start()
    {
        currentRoundBeginning = Time.time;
    }

    void FixedUpdate()
    {
        RecordPosition();
        if (Input.GetKey(KeyCode.Space)) {
            Clone();
        }
    }

    public GameObject clonePrefab;
    public void Clone() {
        GameObject clone = GameObject.Instantiate(clonePrefab);

        var CloneBehaviour = clone.GetComponent<CloneBehaviour>();
        CloneBehaviour.RestartRound(this.currentRoundBeginning);
        CloneBehaviour.trajectory =
            new List<(float, Vector3)>(this.trajectory);
    }

    private void RecordPosition() {
        trajectory.Add((Time.time - currentRoundBeginning, transform.position));
    }

    // [HideInInspector]
    public float currentRoundBeginning;

    public float roundDuration = 10;
    public bool shouldRecord;
    public bool shouldReproduce;

    private List<(float, Vector3)> trajectory
      = new List<(float, Vector3)>();

}
