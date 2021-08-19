using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private Vector3 savedPosition;
    private Vector3 savedVelocity;

    public void Save() {
        savedPosition = GetComponent<Transform>().position;
        savedVelocity = GetComponent<Rigidbody>().velocity;
    }

    public void Load() {
        GetComponent<Transform>().position = savedPosition;
        GetComponent<Rigidbody>().velocity = savedVelocity;
    }

    void Start() {
        Save();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Save();
        } 
        if (Input.GetKeyDown(KeyCode.R)) {
            Load();
        } 
    }
}
