using UnityEngine;

public class BlueprintPickUp : PickupBase
{
    public BlueprintData blueprintData;

    protected override void AddToInventory()
    {
        InventoryManager.Instance.AddBlueprint(blueprintData);
    }

    protected override void AfterPickUP()
    {
        if (blueprintData != null && LevelCompletionManager.Instance != null)
        {
            LevelCompletionManager.Instance.CheckRequiredItem(pickupId.id);
        }
    }
}
