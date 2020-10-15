using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseBall : MonoBehaviour, IRewindable
{
    [SerializeField] private Transform _transform = null;
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private Vector3 _startDirection = Vector3.one;
    [SerializeField] private float _speed = 10f;

    private LinkedList<BaseballPointInTime> _pointsInTime;
    private bool _isRewinding = false;

    public bool IsRewinding { get { return _isRewinding; } set { _isRewinding = value; _rigidbody.isKinematic = !value; } }

    private void OnEnable()
    {
        RewindZone.OnActivateRewindZone += OnActivateRewindZone;
    }    

    private void Start()
    {
        _pointsInTime = new LinkedList<BaseballPointInTime>();
        _rigidbody.velocity = _startDirection.normalized * _speed;
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
            _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
        }
    }

    private void OnDisable()
    {
        RewindZone.OnActivateRewindZone -= OnActivateRewindZone;
    }

    public void Record()
    {
        if (_pointsInTime.Count >= 250)
        {
            _pointsInTime.RemoveFirst();
        }
        _pointsInTime.AddLast((BaseballPointInTime)GetPointInTime());
    }

    public void Rewind()
    {
        if (_pointsInTime.Count > 0)
        {
            var pointInTime = _pointsInTime.Last.Value;
            _transform.position = pointInTime.Pose.position;
            _transform.rotation = pointInTime.Pose.rotation;
            _pointsInTime.RemoveLast();
        }
        else
        {
            _isRewinding = false;
        }
    }

    public PointInTime GetPointInTime()
    {
        Pose pose = new Pose(_transform.position, _transform.rotation);
        return new BaseballPointInTime(pose);
    }

    private void OnActivateRewindZone()
    {
        IsRewinding = true;
    }
}
