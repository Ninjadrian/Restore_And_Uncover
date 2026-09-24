using UnityEngine;

public class ToolPickUp : PickupBase
{
    public ToolData toolData;

    protected override void AddToInventory()
    {
        InventoryManager.Instance.AddTool(toolData);
    }
}
