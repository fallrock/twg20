using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public void Reset() {
        trajectory = new List<Point>();
    }

    public void Put(float time, Vector3 position)
    {
        trajectory.Add(new Point(time, position));
    }

    public void Put(Vector3 position)
    {
        Put(Time.time, position);
    }

    public List<Point> trajectory { get; private set; } =
        new List<Point>();

    public struct Point
    {
        float time { get; set; }
        Vector3 position { get; set; }

        public Point(float time, Vector3 position)
        {
            this.time = time;
            this.position = new Vector3(position.x,
                                        position.y,
                                        position.z);
        }
    }

}
