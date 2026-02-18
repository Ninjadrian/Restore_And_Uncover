using UnityEngine;

public class RecyclablePickUp : MonoBehaviour
{
    public RecyclableData recyclableData;

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
        InventoryManager.Instance.AddRecyclable(recyclableData);

        var idComp = GetComponent<UniquePickupId>();

        if (idComp != null)
        {
            PlayerProfiler.Instance.MarkPickupCollected(idComp.id);
        }

        Destroy(gameObject);
    }
}
