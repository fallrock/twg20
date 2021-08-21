using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{

    // Demonstation
    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            GameObject
                .Find("Managers")
                .GetComponent<CloneManager>()
                .Store(GetComponent<Trajectory>());
        }
    }

}
