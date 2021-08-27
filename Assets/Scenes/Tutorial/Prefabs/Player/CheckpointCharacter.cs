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

    public void Save(Vector3 position)
    {
        Vector3 characterPosition = transform.position;

        player.transform.position = position;
        transform.position = characterPosition;

        ClearClones();
    }

    private void ClearClones()
    {
        gameObject.GetComponent<TrajectoryRecorder>().Reset();
        // unfortunately, it kills all previous clones
        // everytime player respawns
        // player.GetComponent<CloneManager>().Clear();
        ///TODO: fix it
    }
}
