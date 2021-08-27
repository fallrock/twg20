using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeShaderHelper : MonoBehaviour
{
    void Start() {
        float scale = transform.lossyScale.x;
        var renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_Scale", scale);
        // renderer.material.SetColor("_Color", Random.ColorHSV(0f,1f,1f,1f,1f,1f));
    }

    void Update() {
        
    }
}
