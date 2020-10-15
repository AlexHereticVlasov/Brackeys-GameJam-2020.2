using System;

public class ObjectiveItem : BaseCollectable
{
    public static event Action OnObjectiveCollected;

    protected override void Collect()
    {
        OnObjectiveCollected?.Invoke();
    }
}
