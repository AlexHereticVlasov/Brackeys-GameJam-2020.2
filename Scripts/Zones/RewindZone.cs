using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindZone : DeactivalibleZone
{
    private const int RESTORATION_TIME = 6;

    public static event Action OnActivateRewindZone;

    protected override void ActivateZoneEffect(Player player)
    {
        OnActivateRewindZone?.Invoke();
        SetActive(false, Unactive);
        StartCoroutine(Restore(RESTORATION_TIME));
    }

    protected override void DeactivateZoneEffect(Player player)
    {
        return;
    }

    private IEnumerator Restore(float timeToRestore)
    {
        yield return new WaitForSeconds(timeToRestore);
        SetActive(true, Active);
    }
}
