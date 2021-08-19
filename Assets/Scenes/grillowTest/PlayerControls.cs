using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject explosionPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            GameObject.Instantiate(explosionPrefab,
                                   transform.position,
                                   new Quaternion()
            );
        }
    }
}
