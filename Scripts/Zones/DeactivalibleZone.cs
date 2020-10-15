using UnityEngine;

public abstract class DeactivalibleZone : BaseZone
{
    [SerializeField] private Renderer _renderer = null;
    [SerializeField] private Collider _collider = null;
    [SerializeField] protected Material Active = null;
    [SerializeField] protected Material Unactive = null;

    [SerializeField] protected bool IsActive = true;

    private void Awake()
    {
        _renderer.material = IsActive ? Active : Unactive;
        _collider.enabled = IsActive;
    }

    protected abstract override void ActivateZoneEffect(Player player);

    protected abstract override void DeactivateZoneEffect(Player player);

    protected virtual void SetActive(bool isActive, Material material)
    {
        IsActive = isActive;
        _renderer.material = material;
        _collider.enabled = isActive;
    }

}
