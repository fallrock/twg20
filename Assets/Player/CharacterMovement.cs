using System;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float baseAcceleration = 50f;
    // public float maxSpeed = 50f;
    public float steerAcceleration = 10f;
    public float jumpImpulse = 5f;
    [Range(0f, 1f)] public float steerSmoothing = 0.95f;
    [Range(0f, 1f)] public float friction = 0.25f;
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
        if (onGround == 1 && Input.GetKeyDown(KeyCode.Space)) {
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            velocity.y = jumpImpulse;
            GetComponent<Rigidbody>().velocity = velocity;
        }
    }

    private void FixedUpdate() {
        Vector3 f = GetInputForce();
        if (f != Vector3.zero) {
            // GetComponent<Rigidbody>().AddForce(f * GetCurrentAcceleration(f));
            ApplySteering(f);
        }
        ApplyFriction();
    }

    private Vector3 GetInputForce() {
        Vector3 input = GetMovementInput();
        input.y = 0f;
        if (input == Vector3.zero) { return Vector3.zero; }
        input.Normalize();

        Vector3 forward = Vector3.ProjectOnPlane(directionProvider.forward, Vector3.up).normalized;
        Vector3 right = directionProvider.right;
        Vector3 forceDirection = input.z * forward + input.x * right;
        return forceDirection;
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

    // private float GetCurrentAcceleration(Vector3 forceDirection) {
    //     Vector3 velocity = GetComponent<Rigidbody>().velocity;
    //     Vector3 velocityDirection = velocity.normalized;
    //
    //     float currentSpeed = velocity.magnitude;
    //     float speedRatio = Mathf.Clamp(currentSpeed / maxSpeed, 0f, 1f);
    //
    //     // cos == 1 when facing backwards, -1 when facing forward
    //     float cos = -Vector3.Dot(velocityDirection, forceDirection);
    //     // float dirRatio = Mathf.Clamp(cos + 1f, 0f, 1f);  // Anything above 90deg is 1
    //     float dirRatio = (cos + 1f) / 2f; // 1 backwards, 0 forwards
    //     float mod = 1f - speedRatio * (1f - dirRatio);
    //
    //     // Debug.Log($"mod: {Mathf.Round(mod * 100f)}%, speedRatio: {Mathf.Round(currentSpeed / maxSpeed * 100f)}%");
    //     return baseAcceleration * mod;
    // }

    private void ApplySteering(Vector3 wantedDirection) {
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
        allowedAngle *= Mathf.Pow(Mathf.Clamp(Vector3.Dot(wantedDirection, currentDirection) + 1f, 0f, 1f), 0.5f);
        float t = Mathf.Clamp(allowedAngle / wantedAngle, 0f, 1f);

        rb.velocity = Vector3.Slerp(currentVelocity, smoothWantedDirection * currentSpeed, t);
        Vector3 acceleration = Vector3.Project(wantedDirection, currentDirection) * baseAcceleration;
        rb.AddForce(acceleration, ForceMode.Acceleration);
        rb.velocity += savedY * Vector3.up;

        Debug.Log($"speed: {currentVelocity.magnitude.ToString("0.00")}, allowedAngle: {allowedAngle.ToString("0.000")}, acceleration: {acceleration.magnitude.ToString("0.0")}");
    }

    private void ApplyFriction() {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(-rb.velocity * friction, ForceMode.Acceleration);
    }
}
