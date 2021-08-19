using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public float mouseSmoothing = 4f;

    private float clampInDegreesY = 179f;
    private Vector2 _mouseAbsolute;
    private Vector2 _smoothMouse;
    private Quaternion targetOrientation;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0f;
        targetOrientation = Quaternion.Euler(eulerAngles);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
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
}
