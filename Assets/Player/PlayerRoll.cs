using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    public Rigidbody velocityProvider;

    void Update() {
        Vector3 velocity = velocityProvider.velocity;
        float speed = velocity.magnitude;
        if (speed == 0f) {
            return;
        }
        Vector3 direction = velocity.normalized;

        float angle = speed * Mathf.PI * 200f * Time.deltaTime;
        GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle, -Vector3.Cross(direction, Vector3.up)) * GetComponent<Transform>().rotation;
    }
}
