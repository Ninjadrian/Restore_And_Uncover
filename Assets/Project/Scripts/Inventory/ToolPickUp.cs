using UnityEngine;

public class ToolPickUp : MonoBehaviour
{
    public ToolData toolData;

    public void PickUp()
    {
        InventoryManager.Instance.AddTool(toolData);

        var idComp = GetComponent<UniquePickupId>();

        if (idComp != null)
        {
            PlayerProfiler.Instance.MarkPickupCollected(idComp.id);
        }

        Destroy(gameObject);
    }
}
