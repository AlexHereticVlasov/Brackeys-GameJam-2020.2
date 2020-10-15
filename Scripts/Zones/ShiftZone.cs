using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftZone : ConstantZone
{
    [SerializeField] private Rigidbody _playersBody = null;
    [SerializeField] private Vector3 _direction = Vector3.forward;

    private void FixedUpdate()
    {
        if (_playersBody)
            _playersBody.velocity = _direction;
    }

    protected override void ActivateZoneEffect(Player player)
    {
        _playersBody = player.GetComponent<Rigidbody>();
    }

    protected override void DeactivateZoneEffect(Player player)
    {
        _playersBody.velocity = Vector3.zero;
        _playersBody = null;
    }
}
