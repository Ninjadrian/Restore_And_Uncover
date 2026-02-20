using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class PlayerProfiler : MonoBehaviour
{
    public static PlayerProfiler Instance;

    private HashSet<string> collectedSet = new HashSet<string>();

    private static string fileName = "player_profile.json";

    [SerializeField]
    public class PlayerData
    {
        public int level = 0;
        public int time = 0;
        public int day = 0;

        public List<string> ownedTools = new List<string>();

        //Objetos ya recogidos
        public List<string> collectedPickups = new List<string>();  
    }

    public PlayerData data = new PlayerData();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private static string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public void SaveProfile() {
        
        //Limpiamos la lista
        data.ownedTools.Clear();

        //Guardamos las herramientas
        foreach (ToolData tool in InventoryManager.Instance.inventoryTools)
        {
            data.ownedTools.Add(tool.id);
        }

        data.collectedPickups.Clear();
        data.collectedPickups.AddRange(collectedSet);

        //Guardamos máscaras de limpieza
        foreach (var surface in FindObjectsByType<CleanableSurface>(FindObjectsSortMode.None))
            surface.SaveMaskToDisc();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetFilePath(), json);
    }

    public void LoadProfile()
    {
        string path = GetFilePath();

        if (!File.Exists(path))
        {
            SaveProfile();
            return;
        }

        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<PlayerData>(json);

        collectedSet = new HashSet<string>(data.collectedPickups);

        RestoreInventory();
    }

    private void RestoreInventory()
    {
        InventoryManager.Instance.inventoryTools.Clear();

        foreach (string toolId in data.ownedTools)
        {
            ToolData tool = ToolDatabase.instance.GetTool(toolId);
            if (tool != null)
                InventoryManager.Instance.inventoryTools.Add(tool);
        }
    }

    public void MarkPickupCollected(string pickupId)
    {
        if (string.IsNullOrEmpty(pickupId)) return;

        collectedSet.Add(pickupId);
    }

    public bool IsPickUpCollected(string pickupId)
    {
        return !string.IsNullOrEmpty(pickupId) && collectedSet.Contains(pickupId);
    }

    public void StartNewGame()
    {
        data = new PlayerData();
        collectedSet.Clear();

        DeleteAllCleanMasks();

        SaveProfile();
    }

    private void DeleteAllCleanMasks()
    {
        string dir = Application.persistentDataPath;
        var files = Directory.GetFiles(dir, "cleanmask_*.png");

        foreach (var f in files)
            File.Delete(f);
    }
}
