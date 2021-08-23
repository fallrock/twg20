using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollZoom : MonoBehaviour
{
    public float sensitivity = 0.5f;
    public float maxScroll = Mathf.Infinity;

    private Vector3 initialLocalPosition;
    private float totalScroll = 1f;

    void Start() {
        initialLocalPosition = transform.localPosition;
    }

    void Update() {
        totalScroll += -Input.mouseScrollDelta.y * sensitivity;
        totalScroll = Mathf.Clamp(totalScroll, 0f, maxScroll);
        transform.localPosition = initialLocalPosition * totalScroll;
        if (Input.GetKey(KeyCode.Mouse2)) {
            totalScroll = 1f;
        }
    }
}
