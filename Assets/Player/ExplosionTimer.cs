using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTimer : MonoBehaviour
{
    public float duration = 2.0f;

    private float lastExplosion;

    public bool Ready
    {
        get { return lastExplosion + duration <= Time.time; }
    }

    void Start()
    {
        // spawn explosions allowed
        Reset();
    }

    public void Set()
    {
        lastExplosion = Time.time;
    }

    // Make it available now
    public void Reset()
    {
        lastExplosion = Time.time - duration;
    }

}
