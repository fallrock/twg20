using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{

    // Demonstation
    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            cloneManager.GetComponent<CloneManager>()
                .Store(GetComponent<Trajectory>());
        }
    }

    private GameObject cloneManager;

    void Start()
    {
        cloneManager = GameObject.Find("CloneManager");
    }

}
