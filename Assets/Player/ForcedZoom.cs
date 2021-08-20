using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedZoom : MonoBehaviour
{
    private Vector3 initialLocalPosition;

    void Start() {
        initialLocalPosition = transform.localPosition;
    }

    void Update() {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        Vector3 origin = transform.parent.position;
        Vector3 target = transform.position;
        Vector3 toTarget = (target - origin);

        float maxDistance = toTarget.magnitude;

        RaycastHit hit;
        if (Physics.Raycast(origin, toTarget.normalized, out hit, maxDistance, layerMask)) {
            transform.position = hit.point;
        } else {
            transform.localPosition = initialLocalPosition;
        }
    }
}
