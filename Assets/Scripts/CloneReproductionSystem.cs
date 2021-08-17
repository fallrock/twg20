using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneReproductionSystem : MonoBehaviour
{
    void Start()
    {
        currentRoundBeginning = Time.time; // remember to re-set it
    }

    void Update()
    {
        // Debug.Log("-----------");
        // foreach (KeyValuePair<float, Vector3> kvp in positionRecords)
        // {
        //     Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value.ToString()));
        // }
        // Debug.Log("-----------");
    }

    public void RecordPlayerPosition(Vector3 position) {
        positionRecords.Add(Time.time, position);
    }

    public void RecordExplosion(Vector3 position) {
        explosionRecords.Add(Time.time, position);
    }

    [HideInInspector]
    public float currentRoundBeginning;

    public float roundDuration = 30;

    private SortedDictionary<float, Vector3> positionRecords = new SortedDictionary<float, Vector3>();

    private SortedDictionary<float, Vector3> explosionRecords = new SortedDictionary<float, Vector3>();
}
