using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 respawnOffset;
    public Vector3 respawnPoint
    {
        get { return transform.position + respawnOffset; }
    }

    private void OnTriggerEnter(Collider other)
    {
        var checkpoint =
            other
            .gameObject
            .GetComponent<CheckpointCharacter>();

        if (checkpoint != null)
        {
            checkpoint.Save(gameObject);
        }
    }
}
