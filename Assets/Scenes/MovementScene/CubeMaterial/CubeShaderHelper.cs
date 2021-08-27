using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeShaderHelper : MonoBehaviour
{
    void Start() {
        float scale = transform.lossyScale.x;
        GetComponent<Renderer>().material.SetFloat("_Scale", scale);
    }

    void Update() {
        
    }
}
