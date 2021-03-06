using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float steeringForwardAcceleration = 5f;
    public float baseRawAcceleration = 8f;
    public float deceleration = 10f;
    public float rawAccelerationMaxSpeed = 5f;
    public float maxPreciseSpeed = 0.1f;
    public float steeringMinSpeed = 3f;
    public float steerAcceleration = 15f;
    public float jumpImpulse = 5f;
    public float minWallJumpSpeed = 3f;
    public float maxGroundAngle = 60f;
    [Range(0f, 1f)]
    public float steerSmoothing = 0.95f;
    public float afkDeceleration = 1f;
    [Range(0f, 1f)]
    public float friction = 0.005f;
    public Transform directionProvider;

    private bool onGround = false;

    void Start() {
    }

    void OnCollisionEnter(Collision collision) {
        if (Input.GetKey(KeyCode.Space)) {
            Vector3 velocity = collision.relativeVelocity;
            for (int i = 0; i < collision.contactCount; i++) {
                Vector3 normal = collision.GetContact(i).normal;
                if (Vector3.Dot(velocity, normal) >= this.minWallJumpSpeed) {
                    Jump();
                    return;
                }
            }
        }
    }

    void OnCollisionStay(Collision collision) {
        for (int i = 0; i < collision.contactCount; i++) {
            Vector3 normal = collision.GetContact(i).normal;
            float maxAngleRadians = this.maxGroundAngle / 180f * Mathf.PI;
            if (Vector3.Dot(normal, Vector3.up) >= Mathf.Cos(maxAngleRadians)) {
                this.onGround = true;
                return;
            }
        }
    }

    void Update() {
        if (this.onGround && Input.GetKey(KeyCode.Space)) {
            Jump();
        }
    }

    void FixedUpdate() {
        Vector3 f = GetWorldSpaceInput();
        if (f != Vector3.zero) {
            float speed = Vector3.ProjectOnPlane(GetComponent<Rigidbody>().velocity, Vector3.up).magnitude;
            float t = Mathf.Clamp01(Mathf.InverseLerp(this.steeringMinSpeed, this.rawAccelerationMaxSpeed, speed));
            ApplyLowSpeedAcceleration(f, 1f - t);
            ApplySteering(f, t);
        }
        if (this.onGround) {
            ApplyFriction();
            if (f == Vector3.zero) {
                ApplyAfkDeceleration();
            }
        }
        onGround = false;
    }

    void Jump() {
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        if (velocity.y <= this.jumpImpulse) {
            velocity.y = this.jumpImpulse;
            GetComponent<Rigidbody>().velocity = velocity;
        }
    }

    Vector3 GetWorldSpaceInput() {
        Vector3 input = GetInput();
        input.y = 0f;
        if (input == Vector3.zero) { return Vector3.zero; }
        input.Normalize();

        Vector3 forward = Vector3.ProjectOnPlane(this.directionProvider.forward, Vector3.up).normalized;
        Vector3 right = this.directionProvider.right;
        Vector3 forceDirection = input.z * forward + input.x * right;
        return forceDirection;
    }

    static Vector3 GetInput() {
        Vector3 ret = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { ret.z += 1; }
        if (Input.GetKey(KeyCode.S)) { ret.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { ret.x += 1; }
        if (Input.GetKey(KeyCode.A)) { ret.x -= 1; }
        if (Input.GetKey(KeyCode.Space)) { ret.y += 1; }
        if (Input.GetKey(KeyCode.LeftControl)) { ret.y -= 1; }
        return ret;
    }

    float MixDeceleration(float acceleration, Vector3 wantedDirection) {
        Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
        if (currentVelocity == Vector3.zero) {
            return acceleration;
        }
        Vector3 currentDirection = currentVelocity.normalized;
        float t = Mathf.Clamp01(-Vector3.Dot(wantedDirection, currentDirection));
        return Mathf.Lerp(acceleration, this.deceleration * Mathf.Sign(acceleration), t);
    }

    void ApplyLowSpeedAcceleration(Vector3 forceDirection, float scale) {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 currentVelocity = rb.velocity;
        currentVelocity.y = 0f;

        float currentSpeed = currentVelocity.magnitude;

        float acceleration = this.baseRawAcceleration * scale;
        acceleration = MixDeceleration(acceleration, forceDirection);
        acceleration *= Mathf.Lerp(0.5f, 1f, Mathf.Pow(Mathf.InverseLerp(0f, this.maxPreciseSpeed, currentSpeed), 2f));
        rb.AddForce(forceDirection * acceleration, ForceMode.Acceleration);
    }

    void ApplySteering(Vector3 wantedDirection, float scale) {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 currentVelocity = rb.velocity;
        float savedY = currentVelocity.y;
        currentVelocity.y = 0f;  // Ignore the third dimension in all calculations

        Vector3 currentDirection = currentVelocity;
        currentDirection.y = 0f;
        if (currentDirection == Vector3.zero) {
            return;
        }
        currentDirection.Normalize();
        Vector3 smoothWantedDirection = Vector3.Slerp(wantedDirection, currentDirection, this.steerSmoothing);

        var currentRotation = Quaternion.LookRotation(currentDirection);
        var wantedRotation = Quaternion.LookRotation(smoothWantedDirection);

        float wantedAngle = Quaternion.Angle(currentRotation, wantedRotation);
        float currentSpeed = currentVelocity.magnitude;

        float allowedAngle = this.steerAcceleration / currentSpeed;  // Physically accurate
        allowedAngle = Mathf.Clamp(allowedAngle, 0f, 90f);  // For scale to work
        allowedAngle *= Mathf.Pow(Mathf.Clamp01(Vector3.Dot(wantedDirection, currentDirection) + 1f), 0.5f);
        allowedAngle *= scale;
        float t = Mathf.Clamp01(wantedAngle != 0f ? allowedAngle / wantedAngle : 1f);

        rb.velocity = Vector3.Slerp(currentVelocity, smoothWantedDirection * currentSpeed, t);
        float acceleration = Vector3.Dot(wantedDirection, currentDirection) * this.steeringForwardAcceleration;
        acceleration = MixDeceleration(acceleration, wantedDirection);
        if (this.onGround || Vector3.Dot(wantedDirection, currentDirection) < 0f) {
            rb.AddForce(currentDirection * acceleration * scale, ForceMode.Acceleration);
        }
        rb.velocity += savedY * Vector3.up;
    }

    void ApplyFriction() {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(-rb.velocity * this.friction, ForceMode.VelocityChange);
    }

    void ApplyAfkDeceleration() {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.magnitude <= this.afkDeceleration * Time.fixedDeltaTime) {
            rb.velocity = Vector3.zero;
        }
        rb.AddForce(-rb.velocity.normalized * this.afkDeceleration, ForceMode.Acceleration);
    }

}
