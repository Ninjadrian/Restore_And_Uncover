using UnityEngine;

public class BlueprintPickUp : MonoBehaviour
{
    public BlueprintData blueprintData;

    private void Awake()
    {
        var idComp = GetComponent<UniquePickupId>();
        if (idComp != null && PlayerProfiler.Instance.IsPickUpCollected(idComp.id))
        {
            Destroy(gameObject);
        }
    }

    public void PickUp()
    {
        InventoryManager.Instance.AddBlueprint(blueprintData);

        var idComp = GetComponent<UniquePickupId>();

        if (idComp != null)
        {
            PlayerProfiler.Instance.MarkPickupCollected(idComp.id);
        }

        LevelCompletionManager.Instance.CheckRequiredItem(idComp.id);
        Destroy(gameObject);
    }
}
