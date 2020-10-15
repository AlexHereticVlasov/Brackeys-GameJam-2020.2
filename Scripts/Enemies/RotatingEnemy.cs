using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : MovingEnemy, IRotatable
{
    [SerializeField] private Transform _point = null;
    [SerializeField] private Vector3 _axis = Vector3.up;

    private LinkedList<RotatingPointInTime> _pointsInTime;
    private bool _isRewinding = false;

    public override bool IsRewinding { set { _isRewinding = value; } get { return _isRewinding; } }

    private void Awake()
    {
        _pointsInTime = new LinkedList<RotatingPointInTime>();
        RecalculateSpeed();
    }

    private void FixedUpdate()
    {
        if (_isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
            Move();
        }
    }

    public void Move()
    {
        MyTransform.RotateAround(_point.position, _axis, Speed);
    }

    public override PointInTime GetPointInTime()
    {
        RotatingPointInTime pointInTime = new RotatingPointInTime(new Pose(MyTransform.position, MyTransform.rotation));
        return pointInTime;
    }

    public override void Record()
    {
        if (_pointsInTime.Count >= 250)
        {
            _pointsInTime.RemoveFirst();
        }
        _pointsInTime.AddLast((RotatingPointInTime)GetPointInTime());
    }

    public override void Rewind()
    {
        if (_pointsInTime.Count > 0)
        {
            RotatingPointInTime pointInTime = _pointsInTime.Last.Value;
            MyTransform.position = pointInTime.Pose.position;
            MyTransform.rotation = pointInTime.Pose.rotation;
            _pointsInTime.RemoveLast();
        }
        else
        {
            _isRewinding = false;
        }
    }
}
