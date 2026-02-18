using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<ToolData> startingTools;
    public List<ToolData> inventoryTools = new List<ToolData>();

    public BlueprintData startingBlueprint;
    public List<BlueprintData> unlockedBlueprints = new List<BlueprintData>();

    public List<RecyclableData> recyclables = new List<RecyclableData>();

    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else 
        { 
            Destroy(gameObject); 
        }
    }

    public void InitializeInventory()
    {
        //Herramientas
        inventoryTools.Clear();

        //foreach (ToolData tool in startingTools)
        //{
        //    inventory.Add(tool);
        //}

        inventoryTools.AddRange(startingTools);

        //Planos
        unlockedBlueprints.Clear();
        unlockedBlueprints.Add(startingBlueprint);

        OnInventoryChanged?.Invoke();
    }

    //Herramientas
    public void AddTool(ToolData tool)
    {
        inventoryTools.Add(tool);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveTool(ToolData tool)
    {
        inventoryTools.Remove(tool);
        OnInventoryChanged?.Invoke();
    }


    //Reciclables
    public int GetMaterialCount(MaterialType type)
    {
        int count = 0;

        foreach (RecyclableData item in recyclables)
        {
            if (item.materialType == type) count++;
        }

        return count;
    }

    public void AddRecyclable(RecyclableData recyclableData)
    {
        recyclables.Add(recyclableData);
    }
}
