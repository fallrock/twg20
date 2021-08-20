using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{

    // Demonstation
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            InstantiateClones();
        }
    }

    public GameObject clonePrefab;

    public void InstantiateClones() {
        foreach (var trajectory in clones)
        {
            GameObject clone = GameObject.Instantiate(clonePrefab);

            clone.GetComponent<Trajectory>().Set(trajectory);
        }
    }

    public void KillAllClones() {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject clone in clones) {
            GameObject.Destroy(clone);
        }
    }

    public void Store(Trajectory trajectory) {
        clones.Add(new List<Trajectory.Point>(trajectory.trajectory));
    }

    private List<List<Trajectory.Point>> clones
      = new List<List<Trajectory.Point>>();
}