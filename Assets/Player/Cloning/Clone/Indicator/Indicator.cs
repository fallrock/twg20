using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public float duration = 1.0f;

    public float rotationFactor = 1.0f;
    public float scaleFactor = 1.0f;

    public void Initialize(float endTime)
    {
        this.endTime = Time.time + endTime;
    }

    private float endTime;

    void Update()
    {
        float x = Time.time;
        float start = endTime - duration;

        float scale = Step(0, x - start) * Step(x - start, duration) * (1 - Mathf.Pow((( x - start) / duration), 2));

        scale *= scaleFactor;

        transform.localScale = new Vector3(scale, transform.localScale.y, scale);
    }

    private float Step(float a, float b)
    {
        if (b > a) return 1.0f;
        return 0.0f;
    }

}
