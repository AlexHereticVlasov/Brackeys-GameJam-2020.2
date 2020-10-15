using System;
using UnityEngine;

public class PortalZone : DeactivalibleZone
{
    public static event Action OnLevelComplite;

    [SerializeField] private ParticleSystem _particles = null;

    private int _objectivesLeft = 0;

    private void Start()
    {
        _objectivesLeft = FindObjectsOfType<ObjectiveItem>().Length;
    }

    private void OnEnable()
    {
        ObjectiveItem.OnObjectiveCollected += OnObjectiveCollected;
    }

    private void OnDisable()
    {
        ObjectiveItem.OnObjectiveCollected -= OnObjectiveCollected;
    }

    private void OnObjectiveCollected()
    {
        _objectivesLeft--;
        if (_objectivesLeft <= 0)
        {
            SetActive(true, Active);
        }
    }

    protected override void ActivateZoneEffect(Player player)
    {
        Debug.Log("Level Complite");
        OnLevelComplite?.Invoke();
    }

    protected override void DeactivateZoneEffect(Player player)
    {
        return;
    }

    protected override void SetActive(bool isActive, Material material)
    {
        base.SetActive(isActive, material);
        _particles.Play();
    }
}
