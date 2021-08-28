using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streamscript : MonoBehaviour
{
    public Transform mParticleSystem;

    private MeshRenderer mMeshRenderer;

    void Awake()
    {
        mMeshRenderer = GetComponent<MeshRenderer>();
    }
    void OnTriggerStay(Collider other)

    {


        UpdateStream(GetHeight(other));

    }
    void OnTriggerExit(Collider other)
    {
        UpdateStream(0);
    }
    private float GetHeight(Collider collider)
    {
        return collider.transform.position.y + collider.bounds.size.y / 2;
    }
    private void UpdateStream(float newHeight)
    { //particle
        Vector3 newPosition = new Vector3(transform.position.x, newHeight, transform.position.z);
        mParticleSystem.position = newPosition;

        // Height cutoff
        //newHeight /= transform.localScale.y;
        float myHeight = GetComponent<Collider>().bounds.size.y + transform.position.y;
        Debug.Log(newHeight);
        mMeshRenderer.material.SetFloat("_Cutoff", newHeight / myHeight);
    }
}
