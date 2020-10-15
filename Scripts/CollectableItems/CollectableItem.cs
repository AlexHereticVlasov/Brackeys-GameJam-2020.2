using System;

public class CollectableItem : BaseCollectable
{
    public static event Action OnItemCollect;

    protected override void Collect()
    {
        OnItemCollect?.Invoke();
    }
}
