using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    public GameObject clonePrefab;

    public void InstantiateClones() {
        foreach (var trajectory in trajectories) {
            GameObject clone = GameObject.Instantiate(this.clonePrefab);
            clone.GetComponent<TrajectoryReproducer>().Initialize(trajectory);
        }
    }

    public void KillAllClones() {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject clone in clones) {
            GameObject.Destroy(clone);
        }
    }

    public void Store(Trajectory trajectory) {
        trajectories.Add(trajectory);
    }

    public void Clear() {
        trajectories.Clear();
    }

    private List<Trajectory> trajectories = new List<Trajectory>();
}
