using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public float duration = 1.0f;

    public float rotationFactor = 1.0f;
    public float scaleFactor = 1.0f;

    private float endTime;
    private Vector3 originalScale;

    public void Initialize(float endTime)
    {
        this.endTime = Time.time + endTime;
    }

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float x = Time.time;
        float start = endTime - duration;

        float y = Step(0, x - start) * Step(x - start, duration) * Mathf.Sin(Mathf.Pow((x - start) / duration, 8.0f) * Mathf.PI);

        float scale = y * scaleFactor;

        Vector3 newScale = new Vector3(originalScale.x * scale, originalScale.y, originalScale.z * scale);
        transform.localScale = newScale;

        transform.Rotate(new Vector3(0, y * rotationFactor, 0));
    }

    private float Step(float a, float b)
    {
        if (b > a) return 1.0f;
        return 0.0f;
    }

}
