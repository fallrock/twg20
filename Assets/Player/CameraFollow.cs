using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0f, 1f)]
    public float lerpSpeed = 0.1f;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target) {
            ///TODO frameRate independency
            transform.position = Vector3.Lerp(transform.position, target.position, lerpSpeed);
        }
    }
}
