using UnityEngine;

public class CollectablePickUp : PickupBase
{
    public CollectableData collectableData;

    protected override void AddToInventory()
    {
        InventoryManager.Instance.AddCollectable(collectableData);
    }

    protected override void AfterPickUP()
    {
        if (pickupId != null && LevelCompletionManager.Instance != null)
        {
            LevelCompletionManager.Instance.CheckRequiredItem(pickupId.id);
        }
    }
}
