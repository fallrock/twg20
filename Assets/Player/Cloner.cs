using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{
    void Start()
    {
        cloneManager = GameObject.Find("CloneManager");
        currentRoundBeginning = Time.time;
    }

    // Demonstation
    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            Save();
        }
    }

    void FixedUpdate()
    {
        RecordPosition();
    }

    public void Save() {
        var manager = cloneManager.GetComponent<CloneManager>();
        manager.Store(this.trajectory);
    }

    private void RecordPosition() {
        trajectory.Add((Time.time - currentRoundBeginning,
                        transform.position));
    }

    [HideInInspector]
    public float currentRoundBeginning;

    private GameObject cloneManager;

    private List<(float, Vector3)> trajectory
      = new List<(float, Vector3)>();

}
