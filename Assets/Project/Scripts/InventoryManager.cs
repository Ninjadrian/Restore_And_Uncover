using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<ToolData> startingTools;

    public List<ToolData> inventory = new List<ToolData>();

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
        inventory.Clear();

        //foreach (ToolData tool in startingTools)
        //{
        //    inventory.Add(tool);
        //}

        inventory.AddRange(startingTools);
        OnInventoryChanged?.Invoke();
    }

    public void AddTool(ToolData tool)
    {
        inventory.Add(tool);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveTool(ToolData tool)
    {
        inventory.Remove(tool);
        OnInventoryChanged?.Invoke();
    }
}
