using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public void Reset() {
        trajectory = new List<Point>();
    }

    public void Put(Point point)
    {
        trajectory.Add(point);
    }

    public void Set(List<Point> trajectory)
    {
        this.trajectory = new List<Point>(trajectory);
    }

    public List<Point> trajectory { get; private set; } =
        new List<Point>();

    public struct Point
    {
        public float time { get; set; }
        public Vector3 position { get; set; }

        public Point(float time, Vector3 position)
        {
            this.time = time;
            // this.position = new Vector3(position.x,
            //                             position.y,
            //                             position.z);
            this.position = position;
        }

        public static bool operator==(Point left, Point right)
        {
            return
            (
                left.time == right.time
                && left.position == right.position
            );
        }
        public static bool operator!=(Point left, Point right)
        {
            return !(left != right);
        }

    }

}
