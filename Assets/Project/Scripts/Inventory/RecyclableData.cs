using UnityEngine;

[CreateAssetMenu(fileName = "RecyclableData", menuName = "Inventory/Recyclable")]
public class RecyclableData : ItemData
{
    public MaterialType materialType;
}

public enum MaterialType { Metal, Plastic, Cardboard, Electronic }
