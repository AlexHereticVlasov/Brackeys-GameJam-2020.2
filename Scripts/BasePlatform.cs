using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlatform : MonoBehaviour
{
    [SerializeField] private Transform _transform = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            player.transform.SetParent(_transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            if (player.transform.parent == _transform)
                player.transform.SetParent(null);
    }
}
