using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static event Action OnPlayerDied;

    [SerializeField] private Transform _transform = null;
    //[SerializeField] private Collider _collider = null;
    [SerializeField] private LayerMask _mask = 0;
    [SerializeField] private Transform[] _points = null;
    [SerializeField] private float _baseSpeed = 5f;

    private float _speed = 5f;

    private bool _isMovingLeft = false;
    private bool _isMovingRight = false;
    private bool _isMovingDown = false;
    private bool _isMovingUp = false;

    private bool _isAlive = true;

    public float Speed { set { _speed = value; } }

    private void Start()
    {
        _speed = _baseSpeed * Time.fixedDeltaTime;
    }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        if (_isAlive && CheckIsOnEarth())
        {
            if (_isMovingUp)
                Move(_points[0].position, _points[1].position, Vector3.forward);
            else if (_isMovingDown)
                Move(_points[2].position, _points[3].position, Vector3.back);
            if (_isMovingRight)
                Move(_points[1].position, _points[2].position, Vector3.right);
            else if (_isMovingLeft)
                Move(_points[3].position, _points[0].position, Vector3.left);
        }
    }

    private void ReadInput()
    {
        CheckDirection(KeyCode.DownArrow, ref _isMovingDown);
        CheckDirection(KeyCode.UpArrow, ref _isMovingUp);
        CheckDirection(KeyCode.LeftArrow, ref _isMovingLeft);
        CheckDirection(KeyCode.RightArrow, ref _isMovingRight);

        void CheckDirection(KeyCode key, ref bool state)
        {
            if (Input.GetKey(key))
                state = true;
            else if (Input.GetKeyUp(key))
                state = false;
        }
    }

    private void Move(Vector3 point1, Vector3 point2, Vector3 direction)
    {
        if (SeekBorder(point1, direction, 0.25f) && SeekBorder(point2, direction, 0.25f))
            _transform.Translate(direction * _speed, Space.World);
    }

    private bool SeekBorder(Vector3 origin, Vector3 direction, float lenght)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit, lenght, _mask))
            return false;
        return true;
    }

    private bool CheckIsOnEarth()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            if (!SeekBorder(_points[i].position, Vector3.down, 0.55f))
                return true;
        }

        return false;
    }

    internal void Die()
    {
        if (_isAlive)
        {
            _isAlive = false;
            OnPlayerDied?.Invoke();
            Debug.Log("You Died");
        }
    }
}
