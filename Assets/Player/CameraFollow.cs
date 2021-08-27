using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0f, 1f)]
    public float lerpSpeed = 0.1f;

    public Transform target;

    void Update() {
        if (target) {
            float t = 1f - Mathf.Pow(1f - lerpSpeed, Time.deltaTime * 60f);
            transform.position = Vector3.Lerp(transform.position, target.position, t);
        }
    }
}
