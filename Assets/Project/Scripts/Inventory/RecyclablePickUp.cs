using UnityEngine;

public class RecyclablePickUp : PickupBase
{
    public RecyclableData recyclableData;

    protected override void AddToInventory()
    {
        InventoryManager.Instance.AddRecyclable(recyclableData);
    }

    protected override void AfterPickUP()
    {
        if (LevelCompletionManager.Instance != null)
        {
            LevelCompletionManager.Instance.TryCompleteLevel();
        }
    }
}
