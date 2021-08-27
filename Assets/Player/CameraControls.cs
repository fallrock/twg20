using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float mouseSensitivity = 6f;
    public float mouseSmoothing = 3f;

    private float clampInDegreesY = 179f;
    private Vector2 _mouseAbsolute;
    private Vector2 _smoothMouse;
    private Quaternion targetOrientation;

    void Start() {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0f;
        targetOrientation = Quaternion.Euler(eulerAngles);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        input *= mouseSensitivity;
        float t = 1f - Mathf.Pow(1f - 1f / mouseSmoothing, Time.deltaTime * 60f);
        _smoothMouse = Vector3.Lerp(_smoothMouse, input, t);
        _mouseAbsolute += _smoothMouse * Time.deltaTime * 60f;
        if (clampInDegreesY < 360f) {
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegreesY * 0.5f, clampInDegreesY * 0.5f);
        }
        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;
        Quaternion rhs = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
        transform.localRotation *= rhs;
    }
}
