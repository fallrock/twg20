using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCharacter : MonoBehaviour
{
    private GameObject player
    {
        get
        {
            return gameObject.transform.parent.gameObject;
        }
    }

    public void Save(GameObject checkpoint)
    {
        if (checkpoint
            .GetComponent<Collider>()
            .bounds
            .Contains(player.transform.position))
        { return; }

        Vector3 position =
            checkpoint
            .GetComponent<Checkpoint>()
            .respawnPoint;

        MovePlayer(position);

        ClearClones();
    }

    private void MovePlayer(Vector3 position)
    {
        Vector3 characterPosition = transform.position;

        player.transform.position = position;
        transform.position = characterPosition;
    }

    private void ClearClones()
    {
        gameObject.GetComponent<TrajectoryRecorder>().Reset();
        player.GetComponent<CloneManager>().Clear();
    }
}
