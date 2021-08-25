using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trajectory = System.Collections.Generic.List<UnityEngine.Vector3>;

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

    private List<Trajectory> trajectories = new List<Trajectory>();
}
