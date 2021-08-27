using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 respawnOffset;

    private void OnTriggerEnter(Collider other)
    {
        var checkpoint =
            other
            .gameObject
            .GetComponent<CheckpointCharacter>();

        if (checkpoint != null)
        {
            Vector3 position = transform.position + respawnOffset;
            checkpoint.Save(position);
        }
    }
}
