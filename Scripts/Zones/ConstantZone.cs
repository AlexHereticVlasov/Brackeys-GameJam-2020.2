using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConstantZone : BaseZone
{
    protected abstract override void ActivateZoneEffect(Player player);

    protected abstract override void DeactivateZoneEffect(Player player);
}
