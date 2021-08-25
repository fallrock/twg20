using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLightIntensityController : MonoBehaviour
{
    public float intensityMultiplier = 1.0f;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        Light light = GetComponent<Light>();
        float x = Time.time - startTime;
        light.intensity = intensityMultiplier * (
            1.0f / Mathf.Sin(x + 0.05f) - 2.0f
        );
    }
}
