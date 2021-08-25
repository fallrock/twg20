using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollZoom : MonoBehaviour
{
    public float sensitivity = 0.5f;
    public float maxScroll = Mathf.Infinity;
    [Range(0f, 1f)]
    public float smoothing = 0.9f;

    private Vector3 initialLocalPosition;
    private float totalScroll = 1f;
    private float totalScrollSmooth = 1f;

    void Start() {
        initialLocalPosition = transform.localPosition;
    }

    void Update() {
        totalScroll += -Input.mouseScrollDelta.y * sensitivity;
        totalScroll = Mathf.Clamp(totalScroll, 0f, maxScroll);
        totalScrollSmooth = Mathf.Lerp(totalScrollSmooth, totalScroll, 1f - smoothing);
        transform.localPosition = initialLocalPosition * totalScrollSmooth;
        if (Input.GetKey(KeyCode.Mouse2)) {
            totalScroll = 1f;
        }
    }
}
