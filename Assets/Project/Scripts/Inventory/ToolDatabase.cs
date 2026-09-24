using UnityEngine;
using System.Collections.Generic;

public class ToolDatabase : MonoBehaviour
{
    public static ToolDatabase Instance;

    public List<ToolData> allTools;

    private Dictionary<string, ToolData> toolById;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        toolById = new Dictionary<string, ToolData>();

        foreach(var  tool in allTools)
        {
            toolById[tool.id] = tool;
        }
    }

    public ToolData GetTool(string id)
    {
        toolById.TryGetValue(id, out var tool);
        return tool;
    }
}
