using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Inventory/Tool")]
public class ToolData : ScriptableObject
{
    public string id;
    public string toolName;
    public Sprite icon;
    public GameObject toolPrefab;
}
