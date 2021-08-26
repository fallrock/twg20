using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject goal;

    private void GetDestroyed()
    {
        ///TODO: animation
        GameObject.Destroy(goal);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.tag == "Player.Character")
        {
            ///TODO make hierarchy independent
            Transform player = go.transform.parent;
            player.GetComponent<CollectableManager>().Collect();
            GetDestroyed();
        }
    }
}
