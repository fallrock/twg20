using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public ulong collected = 0;
    public ulong toCollect = 1;

    public void Reset()
    {
        collected = 0;
    }

    public void Collect()
    {
        collected++;
        if (collected >= toCollect)
        {
            ///TODO: win the fucking game
            Debug.Log("won");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger entered");
    }

    void Update()
    {
        
    }
}
