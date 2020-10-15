using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer = null;
    [SerializeField] private Collider _collider = null;
    [SerializeField] private GameObject _deathEffect = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Collect();
            SetMeshActivity(false);
        }
    }

    protected abstract void Collect();

    private void SetMeshActivity(bool isActive)
    {
        SpawnEffect();
        _renderer.enabled = isActive;
        _collider.enabled = isActive;
    }

    private void SpawnEffect()
    {
        GameObject effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
    }
}
