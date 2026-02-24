using UnityEngine;

[CreateAssetMenu(fileName = "CollectableData", menuName = "Inventory/Collectable")]
public class CollectableData : ItemData
{
    public CollectableType collectableType;
}

public enum CollectableType { Document, UniqueObject, Insignia}
