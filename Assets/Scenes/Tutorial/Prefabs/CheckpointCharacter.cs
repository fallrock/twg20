using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCharacter : MonoBehaviour
{
    public void Save(Vector3 position)
    {
        Vector3 characterPosition = transform.position;

        GameObject player = gameObject.transform.parent.gameObject;

        player.transform.position = position;
        transform.position = characterPosition;
    }
}
