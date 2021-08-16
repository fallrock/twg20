using System;
using UnityEngine;


public class FreeCamController : MonoBehaviour
{

    private void Start()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0f;
        targetOrientation = Quaternion.Euler(eulerAngles);
        Rigidbody rb = GetComponent<Rigidbody>();
        // if (!rb)
        // {
        //     UnityEngine.Object.Destroy(gameObject);
        //     return;
        // }
        // rb.useGravity = false;
        // rb.freezeRotation = true;
        rb.drag = deceleration;
        // rb.interpolation = RigidbodyInterpolation.Interpolate;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        Vector2 vector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        vector = Vector2.Scale(vector, new Vector2(mouseSensitivity * mouseSmoothing, mouseSensitivity * mouseSmoothing));
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, vector.x, 1f / mouseSmoothing);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, vector.y, 1f / mouseSmoothing);
        _mouseAbsolute += _smoothMouse;
        if (clampInDegreesY < 360f)
        {
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegreesY * 0.5f, clampInDegreesY * 0.5f);
        }
        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;
        Quaternion rhs = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
        transform.localRotation *= rhs;
    }

    private void FixedUpdate()
    {
        Vector3 vector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            vector.z += acceleration;
        }
        if (Input.GetKey(KeyCode.S)) {
            vector.z -= acceleration;
        }
        if (Input.GetKey(KeyCode.D)) {
            vector.x += acceleration;
        }
        if (Input.GetKey(KeyCode.A)) {
            vector.x -= acceleration;
        }
        if (Input.GetKey(KeyCode.Space)) {
            vector.y += acceleration;
        }
        if (Input.GetKey(KeyCode.LeftControl)) {
            vector.y -= acceleration;
        }
        if (vector != Vector3.zero) {
            Vector3 a = Vector3.zero;
            if (!flyForward) {
                float y = vector.y;
                vector.y = 0f;
                vector.Normalize();
                vector *= acceleration;
                a = vector.z * Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
                a += vector.x * transform.right;
                a.y = y;
            } else {
                vector.y = 0f;
                a = transform.TransformDirection(vector);
            }
            GetComponent<Rigidbody>().AddForce(a);
        }
    }

    //public static void Spawn(bool spawn)
    //{
    //    if (spawn && !exists)
    //    {
    //        defaultMainCam = Camera.main;
    //        defaultMainCam.enabled = false;
    //        AudioListener component = defaultMainCam.GetComponent<AudioListener>();
    //        if (component != null)
    //        {
    //            component.enabled = false;
    //        }
    //        freeCamGO = new GameObject();
    //        freeCamGO.AddComponent<Camera>();
    //        freeCamGO.AddComponent<GUILayer>();
    //        freeCamGO.AddComponent<FlareLayer>();
    //        freeCamGO.AddComponent<AudioListener>();
    //        freeCamGO.AddComponent<Rigidbody>();
    //        freeCamGO.transform.position = defaultMainCam.transform.position;
    //        freeCamGO.transform.rotation = defaultMainCam.transform.rotation;
    //        freeCamGO.AddComponent<FreeCamController>();
    //        exists = true;
    //        return;
    //    }
    //    if (!spawn && exists)
    //    {
    //        UnityEngine.Object.Destroy(freeCamGO);
    //        freeCamGO = null;
    //        if (defaultMainCam == null)
    //        {
    //            defaultMainCam = UnityEngine.Object.FindObjectOfType<Camera>();
    //        }
    //        if (defaultMainCam != null)
    //        {
    //            defaultMainCam.enabled = true;
    //            AudioListener component2 = defaultMainCam.GetComponent<AudioListener>();
    //            if (component2 != null)
    //            {
    //                component2.enabled = true;
    //            }
    //        }
    //        return;
    //    }
    //}

    private float rbMass;

    private float clampInDegreesY = 179f;

    private Vector2 _mouseAbsolute;

    private Vector2 _smoothMouse;

    private Quaternion targetOrientation;

    public bool flyForward;

    public float acceleration = 50f;

    public float deceleration = 3f;

    public bool lockCursor = false;

    public float mouseSensitivity = 2f;

    public float mouseSmoothing = 4f;

}
