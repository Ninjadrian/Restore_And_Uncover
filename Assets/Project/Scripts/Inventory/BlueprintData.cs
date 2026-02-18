using UnityEngine;

[CreateAssetMenu(fileName = "BlueprintData", menuName = "Inventory/Blueprint")]
public class BlueprintData : ItemData
{
    public ToolData resultTool;

    public int metalAmount;
    public int plasticAmount;
    public int cardboardAmount;
}
