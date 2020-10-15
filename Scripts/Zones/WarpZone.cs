using System.Collections;
using System;
using UnityEngine;

public class WarpZone : ConstantZone
{
    public static event Action<Transform> OnTeleportation;

    [SerializeField] private Transform _destination = null;
    [SerializeField] private Transform _cameraDestination = null;
    [SerializeField] private float _delay = 0;
    [SerializeField] private GameObject _effect = null;

    protected override void ActivateZoneEffect(Player player)
    {
        StartCoroutine(Teleport(player));
    }

    protected override void DeactivateZoneEffect(Player player)
    {
        return;
    }

    private IEnumerator Teleport(Player player)
    {
        yield return new WaitForSeconds(_delay);
        SpawnEffect(player);
        player.transform.position = _destination.position;
        OnTeleportation?.Invoke(_cameraDestination);
    }

    private void SpawnEffect(Player player)
    {
        GameObject effect = Instantiate(_effect, player.transform.position, Quaternion.identity);
        Destroy(effect, 5f);
    }
}
