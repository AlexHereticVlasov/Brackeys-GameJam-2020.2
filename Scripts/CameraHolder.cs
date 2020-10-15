using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform _transform = null;
    [SerializeField] private Transform _target = null;

    private float _delta = 0;
    private float _speed = 0.25f;

    private void OnEnable()
    {
        CameraMovementZone.OnCameraTargetChange += OnCameraTargetChange;
        CameraMovementZone.OnCameraZoneExit += OnCameraZoneExit;
        WarpZone.OnTeleportation += OnCameraZoneExit;
    }

    private void OnCameraZoneExit(Transform point)
    {
        _target = point;
        _delta = 0;
    }

    private void OnCameraTargetChange(Transform transform1, Transform transform2)
    {
        if (_target == transform1)
            _target = transform2;
        else
            _target = transform1;
        _delta = 0;
    }

    private void Update()
    {
        if (_delta <= 1)
        {
            _delta += Time.deltaTime * _speed;
            _transform.position = Vector3.Lerp(_transform.position, _target.position, _delta);
        }
    }

    private void OnDisable()
    {
        CameraMovementZone.OnCameraTargetChange -= OnCameraTargetChange;
        CameraMovementZone.OnCameraZoneExit -= OnCameraZoneExit;
        WarpZone.OnTeleportation -= OnCameraZoneExit;
    }
}
