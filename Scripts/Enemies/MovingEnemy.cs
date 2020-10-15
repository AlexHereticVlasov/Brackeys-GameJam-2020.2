using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEnemy : BaseEnemy, IRewindable
{
    [SerializeField] protected Transform MyTransform = null;
    [SerializeField] protected float Speed = 5f;

    public abstract bool IsRewinding { set; get; }

    private void OnEnable()
    {
        RewindZone.OnActivateRewindZone += OnActivateRewindZone;
    }

    private void OnDisable()
    {
        RewindZone.OnActivateRewindZone += OnActivateRewindZone;
    }

    private void OnActivateRewindZone()
    {
        IsRewinding = true;
    }

    public abstract PointInTime GetPointInTime();
    public abstract void Record();
    public abstract void Rewind();

    protected void RecalculateSpeed()
    {
        Speed = Speed * Time.fixedDeltaTime;
    }

}
