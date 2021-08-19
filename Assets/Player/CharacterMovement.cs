using System;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float steeringForwardAcceleration = 5f;
    public float baseRawAcceleration = 5f;
    public float rawAccelerationMaxSpeed = 10f;
    public float steeringMinSpeed = 5f;
    public float steerAcceleration = 10f;
    public float jumpImpulse = 5f;
    [Range(0f, 1f)] public float steerSmoothing = 0.95f;
    public float friction = 0.25f;
    public Transform directionProvider;

    private int onGround = 0;

    private void Start() {
    }

    private void OnTriggerEnter(Collider other) {
        onGround++;
    }

    private void OnTriggerExit(Collider other) {
        onGround--;
    }

    private void Update() {
        if (onGround == 1 && Input.GetKey(KeyCode.Space)) {
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            velocity.y = jumpImpulse;
            GetComponent<Rigidbody>().velocity = velocity;
        }
    }

    private void FixedUpdate() {
        Vector3 f = GetWorldSpaceInput();
        if (f != Vector3.zero) {
            float speed = Vector3.ProjectOnPlane(GetComponent<Rigidbody>().velocity, Vector3.up).magnitude;
            float t = Mathf.Clamp01(Mathf.InverseLerp(steeringMinSpeed, rawAccelerationMaxSpeed, speed));
            ApplyLowSpeedAcceleration(f, 1f - t);
            ApplySteering(f, t);
        }
        ApplyFriction();
    }

    private Vector3 GetWorldSpaceInput() {
        Vector3 input = GetInput();
        input.y = 0f;
        if (input == Vector3.zero) { return Vector3.zero; }
        input.Normalize();

        Vector3 forward = Vector3.ProjectOnPlane(directionProvider.forward, Vector3.up).normalized;
        Vector3 right = directionProvider.right;
        Vector3 forceDirection = input.z * forward + input.x * right;
        return forceDirection;
    }

    private static Vector3 GetInput() {
        Vector3 ret = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { ret.z += 1; }
        if (Input.GetKey(KeyCode.S)) { ret.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { ret.x += 1; }
        if (Input.GetKey(KeyCode.A)) { ret.x -= 1; }
        if (Input.GetKey(KeyCode.Space)) { ret.y += 1; }
        if (Input.GetKey(KeyCode.LeftControl)) { ret.y -= 1; }
        return ret;
    }

    private void ApplyLowSpeedAcceleration(Vector3 forceDirection, float scale) {
        Vector3 velocity = GetComponent<Rigidbody>().velocity;

        float currentSpeed = velocity.magnitude;

        float accel = baseRawAcceleration * scale;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(forceDirection * accel, ForceMode.Acceleration);

        Debug.Log($"acceleration: {accel.ToString("0.00")}, speedRatio: {Mathf.Round(scale * 100f)}%");
    }

    private void ApplySteering(Vector3 wantedDirection, float scale) {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 currentVelocity = rb.velocity;
        float savedY = currentVelocity.y;
        currentVelocity.y = 0f;  // Ignore the third dimension in all calculations

        Vector3 currentDirection = currentVelocity;
        currentDirection.y = 0f;
        if (currentDirection != Vector3.zero) {
            currentDirection.Normalize();
        }
        Vector3 smoothWantedDirection = Vector3.Slerp(wantedDirection, currentDirection, steerSmoothing);

        var currentRotation = Quaternion.LookRotation(currentDirection);
        var wantedRotation = Quaternion.LookRotation(smoothWantedDirection);

        float wantedAngle = Quaternion.Angle(currentRotation, wantedRotation);
        float currentSpeed = currentVelocity.magnitude;

        float allowedAngle = steerAcceleration / currentSpeed;  // Physically accurate
        allowedAngle = Mathf.Clamp(allowedAngle, 0f, 90f);  // For scale to work
        allowedAngle *= Mathf.Pow(Mathf.Clamp01(Vector3.Dot(wantedDirection, currentDirection) + 1f), 0.5f);
        allowedAngle *= scale;
        float t = Mathf.Clamp01(wantedAngle != 0f ? allowedAngle / wantedAngle : 1f);

        Debug.Log($"{currentVelocity}, {smoothWantedDirection}, {currentSpeed}, {t}");
        rb.velocity = Vector3.Slerp(currentVelocity, smoothWantedDirection * currentSpeed, t);
        Vector3 acceleration = Vector3.Project(wantedDirection, currentDirection) * steeringForwardAcceleration;
        rb.AddForce(acceleration * scale, ForceMode.Acceleration);
        rb.velocity += savedY * Vector3.up;

        Debug.Log($"speed: {currentVelocity.magnitude.ToString("0.00")}, allowedAngle: {allowedAngle.ToString("0.000")}, acceleration: {acceleration.magnitude.ToString("0.0")}");
    }

    private void ApplyFriction() {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(-rb.velocity * friction, ForceMode.Acceleration);
    }
}
