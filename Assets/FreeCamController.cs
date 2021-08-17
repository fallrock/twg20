using System;
using UnityEngine;


public class FreeCamController : MonoBehaviour
{
    public float baseAcceleration = 50f;
    public float maxSpeed = 50f;
    public float mouseSensitivity = 2f;
    public float mouseSmoothing = 4f;

    private float clampInDegreesY = 179f;
    private Vector2 _mouseAbsolute;
    private Vector2 _smoothMouse;
    private Quaternion targetOrientation;

    private void Start()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0f;
        targetOrientation = Quaternion.Euler(eulerAngles);
        Rigidbody rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 vector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        vector = Vector2.Scale(vector, new Vector2(mouseSensitivity * mouseSmoothing, mouseSensitivity * mouseSmoothing));
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, vector.x, 1f / mouseSmoothing);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, vector.y, 1f / mouseSmoothing);
        _mouseAbsolute += _smoothMouse;
        if (clampInDegreesY < 360f) {
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegreesY * 0.5f, clampInDegreesY * 0.5f);
        }
        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;
        Quaternion rhs = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
        transform.localRotation *= rhs;
    }

    private void FixedUpdate() {
        Vector3 f = GetMovementForce();
        GetComponent<Rigidbody>().AddForce(f);
    }

    private Vector3 GetMovementForce() { 
        Vector3 input = GetMovementInput();
        input.y = 0f;
        if (input == Vector3.zero) { return Vector3.zero; }
        input.Normalize();

        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Vector3 right = transform.right;
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
        float dirRatio = Mathf.Clamp(cos + 1f, 0f, 1f);  // Anything above 90deg is 1
        float mod = 1f - speedRatio * (1f - dirRatio);

        Debug.Log($"mod: {Mathf.Round(mod * 100f)}%, speedRatio: {Mathf.Round(currentSpeed / maxSpeed * 100f)}%");
        return baseAcceleration * mod;
    }

}
