using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnimation : MonoBehaviour
{
    public GameObject model;

    public float rotationSpeed = 5.0f;

    public float amplitude = 5.0f;
    public float speed = 5.0f;

    void FixedUpdate()
    {
        model.transform.Rotate(new Vector3(0, rotationSpeed, 0));

        // y = y0 + amplitude * Mathf.Sin(speed * Time.time);
        // derivative:
        float deltaY =
            amplitude * speed * Mathf.Cos(speed * Time.time)
            * Time.deltaTime;
        model.transform.position += new Vector3(0, deltaY, 0);
    }

}
