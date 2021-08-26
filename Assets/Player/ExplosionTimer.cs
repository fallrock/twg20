using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTimer : MonoBehaviour
{
    public float duration = 5.0f;

    private float lastExplosion;

    public bool Ready
    {
        get { return lastExplosion + duration <= Time.time; }
    }

    void Start()
    {
        // spawn explosions allowed
        lastExplosion = -duration;
    }

    public void Reset()
    {
        lastExplosion = Time.time;
    }

}
