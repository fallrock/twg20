using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingAbyss : MonoBehaviour
{
    public float abyssLevel = -10.0f;
    void Update()
    {
        if (transform.position.y < abyssLevel)
        {
            GetComponent<ExplosiveController>().Explode();
        }
    }
}
