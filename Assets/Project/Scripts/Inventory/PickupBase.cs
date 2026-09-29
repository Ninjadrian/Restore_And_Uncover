using UnityEngine;

public abstract class PickupBase : MonoBehaviour, IInteractable
{
    protected UniquePickupId pickupId;

    protected virtual void Awake()
    {
        pickupId = GetComponent<UniquePickupId>();

        if (pickupId != null && PlayerProfiler.Instance != null && PlayerProfiler.Instance.IsPickUpCollected(pickupId.id))
        {
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        PickUp();
    }

    public void PickUp()
    {
        AddToInventory();

        if (pickupId != null && PlayerProfiler.Instance != null)
        {
            PlayerProfiler.Instance.MarkPickupCollected(pickupId.id);
        }

        AfterPickUP();

        Destroy(gameObject);
    }

    protected abstract void AddToInventory();

    protected virtual void AfterPickUP()
    {

    }
}
