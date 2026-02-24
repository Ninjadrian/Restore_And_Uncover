using UnityEngine;

public class CollectablePickUp : MonoBehaviour
{
    public CollectableData collectableData;

    public GameEvent objectiveCompleted;

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
        InventoryManager.Instance.AddCollectable(collectableData);

        var idComp = GetComponent<UniquePickupId>();

        if (idComp != null)
        {
            PlayerProfiler.Instance.MarkPickupCollected(idComp.id);
        }

        LevelCompletionManager.Instance.CheckRequiredItem(idComp.id);

        Destroy(gameObject);
    }
}
