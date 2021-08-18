using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        ///TODO: remove it
        if (Input.GetKey(KeyCode.O)) {
            InstantiateClones();
        }
    }

    public GameObject clonePrefab;

    public void InstantiateClones() {
        foreach (var trajectory in clones) {
            GameObject clone = GameObject.Instantiate(clonePrefab);
            clone.transform.SetParent(transform);

            var CloneBehaviour = clone.GetComponent<CloneBehaviour>();
            CloneBehaviour.StartRound();
            CloneBehaviour.trajectory =
                new List<(float, Vector3)>(trajectory);
        }
    }

    public void KillAllClones() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void Store(List<(float, Vector3)> trajectory) {
        clones.Add(new List<(float, Vector3)>(trajectory));
    }

    private List<List<(float, Vector3)>> clones
      = new List<List<(float, Vector3)>>();
}
