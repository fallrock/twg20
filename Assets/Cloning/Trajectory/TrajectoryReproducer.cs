using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trajectory = System.Collections.Generic.List<UnityEngine.Vector3>;

public class TrajectoryReproducer : MonoBehaviour
{
    public bool finished { get; private set; } = false;

    private float currentRoundBeginning;
    private Trajectory trajectory;

    public void Initialize(Trajectory trajectory) {
        this.trajectory = trajectory;
    }

    void Start() {
        this.currentRoundBeginning = Time.time;
    }

    void Update() {
        if (this.finished) {  // Not needed
            return;
        }

        float time = Time.time - this.currentRoundBeginning;
        float delta = Time.fixedDeltaTime;
        int i1 = Mathf.FloorToInt(time / delta);
        int i2 = Mathf.CeilToInt(time / delta);

        if (i2 >= this.trajectory.Count) {
            this.finished = true;
            return;
        }

        Vector3 a = this.trajectory[i1];
        Vector3 b = this.trajectory[i2];
        float t = Mathf.InverseLerp(i1, i2, time);
        Vector3 result = Vector3.Lerp(a, b, t);

        transform.position = result;
    }
}
