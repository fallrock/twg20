using System;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float baseAcceleration = 50f;
    public float maxSpeed = 50f;
    public float baseAngle = 45f;
    public Transform directionProvider;

    private void Start() {
    }

    private void Update() {
    }

    private void FixedUpdate() {
        Vector3 f = GetInputForce();
        if (f != Vector3.zero) {
            GetComponent<Rigidbody>().AddForce(f);
            ApplySteering(f.normalized);
        }
    }

    private Vector3 GetInputForce() { 
        Vector3 input = GetMovementInput();
        input.y = 0f;
        if (input == Vector3.zero) { return Vector3.zero; }
        input.Normalize();

        Vector3 forward = Vector3.ProjectOnPlane(directionProvider.forward, Vector3.up).normalized;
        Vector3 right = directionProvider.right;
        Vector3 forceDirection = input.z * forward + input.x * right;
        return forceDirection * GetCurrentAcceleration(forceDirection);
    }

    private static Vector3 GetMovementInput() {
        Vector3 ret = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { ret.z += 1; }
        if (Input.GetKey(KeyCode.S)) { ret.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { ret.x += 1; }
        if (Input.GetKey(KeyCode.A)) { ret.x -= 1; }
        if (Input.GetKey(KeyCode.Space)) { ret.y += 1; }
        if (Input.GetKey(KeyCode.LeftControl)) { ret.y -= 1; }
        return ret;
    }

    private float GetCurrentAcceleration(Vector3 forceDirection) {
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Vector3 velocityDirection = velocity.normalized;

        float currentSpeed = velocity.magnitude;
        float speedRatio = Mathf.Clamp(currentSpeed / maxSpeed, 0f, 1f);

        // cos == 1 when facing backwards, -1 when facing forward
        float cos = -Vector3.Dot(velocityDirection, forceDirection);
        // float dirRatio = Mathf.Clamp(cos + 1f, 0f, 1f);  // Anything above 90deg is 1
        float dirRatio = (cos + 1f) / 2f; // 1 backwards, 0 forwards
        float mod = 1f - speedRatio * (1f - dirRatio);

        Debug.Log($"mod: {Mathf.Round(mod * 100f)}%, speedRatio: {Mathf.Round(currentSpeed / maxSpeed * 100f)}%");
        return baseAcceleration * mod;
    }

    private void ApplySteering(Vector3 wantedDirection) {
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 currentDirection = rb.velocity.normalized;

        var currentRotation = Quaternion.LookRotation(currentDirection);
        var wantedRotation = Quaternion.LookRotation(wantedDirection);

        float angle = Quaternion.Angle(currentRotation, wantedRotation);
        float t = Mathf.Clamp(baseAngle / angle, 0f, 1f);

        rb.velocity = Vector3.Slerp(rb.velocity, wantedDirection * rb.velocity.magnitude, t);
    }
}
