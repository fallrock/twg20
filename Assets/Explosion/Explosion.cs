using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float radius = 7.5f;
    public float power = 1000.0f;

    void Start()
    {
        Explode();
        GameObject.Destroy(gameObject);
    }

    public void Explode() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null) {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }
        }
    }
}
