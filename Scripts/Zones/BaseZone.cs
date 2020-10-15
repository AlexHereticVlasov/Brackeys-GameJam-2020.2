using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            ActivateZoneEffect(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            DeactivateZoneEffect(player);
        }
    }

    protected abstract void ActivateZoneEffect(Player player);
    protected abstract void DeactivateZoneEffect(Player player);
}
