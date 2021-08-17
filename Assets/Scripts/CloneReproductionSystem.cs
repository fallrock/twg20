using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///TODO: unfuck SRP via creating ghost prefab alongside player prefab
public class CloneReproductionSystem : MonoBehaviour
{
    void Start()
    {
        currentRoundBeginning = Time.time;
    }

    void Update()
    {
        if (currentRoundBeginning + roundDuration < Time.time) {
            currentRoundBeginning = Time.time;
        }

        if (shouldRecord && Input.GetKey(KeyCode.Space)) {
            Clone();
        }

        if (shouldRecord) {
            RecordPosition();
        } else if (shouldReproduce) {
            ReproducePosition();
        }

        // Debug.Log("-----------");
        // Debug.Log(gameObject);
        // Debug.Log(positionRecords.Count);
        // foreach (var kvp in positionRecords)
        // {
        //     Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.time, kvp.position.ToString()));
        // }
        // Debug.Log("-----------");
    }

    ///TODO: unfuck SRP via creating ghost prefab alongside player prefab
    public void Clone() {
        GameObject clone = GameObject.Instantiate(gameObject);

        var components = clone.GetComponents(typeof(Component));
        foreach(var c in components) {
            if (!(
                    c is Transform
                    || c is MeshFilter
                    || c is MeshRenderer
                    || c is CloneReproductionSystem
                )) {
                Destroy(c);
            }
        }
        clone.GetComponent<CloneReproductionSystem>().currentRoundBeginning = this.currentRoundBeginning;
        clone.GetComponent<CloneReproductionSystem>().roundDuration = this.roundDuration;
        clone.GetComponent<CloneReproductionSystem>().positionRecords = new List<PositionRecord>(this.positionRecords);
        clone.GetComponent<CloneReproductionSystem>().shouldRecord = false;
        clone.GetComponent<CloneReproductionSystem>().shouldReproduce = true;
    }

    private void ReproducePosition() {
        // choose current position
        ///TODO: optimize
        if (positionRecords.Count == 0) return;
        float currentRoundTime = Time.time - currentRoundBeginning;
        transform.position
            = positionRecords.FindLast(x => x.time < currentRoundTime)
            .position;
    }

    private void RecordPosition() {
        positionRecords.Add(new PositionRecord(Time.time - currentRoundBeginning, transform.position));
    }

    // [HideInInspector]
    public float currentRoundBeginning;

    public float roundDuration = 10;
    public bool shouldRecord;
    public bool shouldReproduce;

    private List<PositionRecord> positionRecords
      = new List<PositionRecord>();

    private struct PositionRecord {
      public PositionRecord(float _time, Vector3 _position) {
        time = _time;
        position = _position;
      }
      public float time;
      public Vector3 position;
    }

}
