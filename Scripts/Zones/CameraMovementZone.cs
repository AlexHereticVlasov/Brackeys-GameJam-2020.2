using System;
using UnityEngine;

public class CameraMovementZone : ConstantZone      
{
    public static event Action<Transform, Transform> OnCameraTargetChange;
    public static event Action<Transform> OnCameraZoneExit;

    [SerializeField] private Transform _first = null;
    [SerializeField] private Transform _second = null;

    protected override void ActivateZoneEffect(Player player)
    {
        OnCameraTargetChange?.Invoke(_first, _second);
    }

    protected override void DeactivateZoneEffect(Player player)
    {
        if (GetDistance(_first)< GetDistance(_second))
            OnCameraZoneExit?.Invoke(_first);
        else
            OnCameraZoneExit?.Invoke(_second);

        float GetDistance(Transform point)
        {
            return Vector3.Distance(point.position, player.transform.position);
        }
    }
}
