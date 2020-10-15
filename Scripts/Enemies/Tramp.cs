using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable : IRotatable
{
    //void Move();
    void Wait();
}

public interface IRotatable
{
    void Move();
}

public enum MovementState
{
    Wait = 0,
    Move = 1
}

public class Tramp : MovingEnemy, IMoveable
{
    [SerializeField] private Transform[] _wayPoints = null;
    [SerializeField] private int _currentPoint= 0;
    [SerializeField] private float _motionDelay = .5f;
    [SerializeField] private MovementState _movementState = MovementState.Move;

    private LinkedList<TrampPointInTime> _pointsInTime;
    private float _currentDelay;
    private bool _isRewinding = false;

    public override bool IsRewinding { set { _isRewinding = value; } get { return _isRewinding; } }

    private void Awake()
    {
        _pointsInTime = new LinkedList<TrampPointInTime>();
        _currentDelay = _motionDelay;
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

            switch (_movementState)
            {
                case MovementState.Wait: Wait(); break;
                case MovementState.Move: Move(); break;
            }
        }
    }

    public void Move()
    {
        MoveToPoint();
        CheckDistance();

        void MoveToPoint()
        {
            MyTransform.position = Vector3.MoveTowards(MyTransform.position, _wayPoints[_currentPoint].position, Speed);
        }

        void CheckDistance()
        {
            if (Vector3.Distance(MyTransform.position, _wayPoints[_currentPoint].position) < Speed)
            {
                MyTransform.position = _wayPoints[_currentPoint].position;
                GoToWaitState();
                UpdatePoint();
            }
        }

        void UpdatePoint()
        {
            _currentPoint++;
            if (_currentPoint >= _wayPoints.Length)
                _currentPoint = 0;
        }

        void GoToWaitState()
        {
            if (_motionDelay > 0)
            {
                _movementState = MovementState.Wait;
                _currentDelay = _motionDelay;
            }
        }
    }

    public void Wait()
    {
        _currentDelay -= Time.fixedDeltaTime;
        if (_currentDelay <= 0)
            _movementState = MovementState.Move;
    }

    public override PointInTime GetPointInTime()
    {
        TrampPointInTime pointInTime = new TrampPointInTime(new Pose(MyTransform.position, MyTransform.rotation), _currentPoint, _currentDelay, _movementState);

        return pointInTime;
    }

    public override void Record()
    {
        if (_pointsInTime.Count >= 250)
        {
            _pointsInTime.RemoveFirst();
        }
        _pointsInTime.AddLast((TrampPointInTime)GetPointInTime());
    }

    public override void Rewind()
    {
        if (_pointsInTime.Count > 0)
        {
            TrampPointInTime pointInTime = _pointsInTime.Last.Value;
            MyTransform.position = pointInTime.Pose.position;
            MyTransform.rotation = pointInTime.Pose.rotation;
            _currentPoint = pointInTime.Point;
            _currentDelay = pointInTime.Delay;
            _movementState = pointInTime.State;
            _pointsInTime.RemoveLast();
        }
        else
        {
            _isRewinding = false;
        }

    }
}
