using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewindable
{
    bool IsRewinding { set; get; }

    void Record();
    void Rewind();
    PointInTime GetPointInTime();
}

public abstract class PointInTime
{
    
}

public class TrampPointInTime :PointInTime
{
    public Pose Pose;
    public int Point;
    public float Delay;
    public MovementState State;


    public TrampPointInTime(Pose pose, int point, float delay, MovementState state)
    {
        Pose = pose;
        Point = point;
        Delay = delay;
        State = state;
    }
}

public class RotatingPointInTime : PointInTime
{
    public Pose Pose;

    public RotatingPointInTime(Pose pose)
    {
        Pose = pose;
    }
}

public class BaseballPointInTime : PointInTime
{
    public Pose Pose;

    public BaseballPointInTime(Pose pose)
    {
        Pose = pose;
    }
}
