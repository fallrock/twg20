using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.tag == "Player.Character")
        {
            ///TODO make hierarchy independent
            Transform player = go.transform.parent;
            player.GetComponent<CollectableManager>().Collect();
            GameObject.Destroy(gameObject);
        }
    }
}
