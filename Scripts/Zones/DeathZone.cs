using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : ConstantZone
{
    protected override void ActivateZoneEffect(Player player)
    {
        player.Die();
    }

    protected override void DeactivateZoneEffect(Player player)
    {
        return;
    }
}
