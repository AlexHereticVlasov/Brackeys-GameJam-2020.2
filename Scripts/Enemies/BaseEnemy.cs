using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _collisionEffect = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Die();
            SpawnDeathEffect(other);
        }
    }

    private void SpawnDeathEffect(Collider other)
    { 
        GameObject effect = Instantiate(_collisionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
    }
}
