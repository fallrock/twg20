using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float radius = 1.0f;
    public float power = 500.0f;

    void Start()
    {
        Explode();
        ///TODO: delete gameObject when animation is done
        // GameObject.Destroy(gameObject);
    }

    public void Explode() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null) {
                rb.AddExplosionForce(power, explosionPos, radius);
            }
        }
    }
}
