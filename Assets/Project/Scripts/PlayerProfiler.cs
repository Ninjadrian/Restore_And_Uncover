using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class PlayerProfiler : MonoBehaviour
{
    public static PlayerProfiler Instance;

    public LevelDatabase levelDatabase;
    public LevelConfigSO CurrentLevelConfig { get; private set; }

    private HashSet<string> collectedSet = new HashSet<string>();

    private HashSet<string> cleanedSurfaceSet = new HashSet<string>();

    private HashSet<string> seenMessagesSet = new HashSet<string>();

    private static string fileName = "player_profile.json";

    [SerializeField]
    public class PlayerData
    {
        public string levelId = "level_01";
        public int time = 0;
        public int day = 0;

        //Herramientas del inventario
        public List<string> ownedTools = new List<string>();

        //Objetos ya recogidos
        public List<string> collectedPickups = new List<string>();

        //Superficies limpias
        public List<string> cleanedSurfaces = new List<string>();

        //Mensajes
        public List<string> seenMessages = new List<string>();
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

        data.cleanedSurfaces.Clear();
        data.cleanedSurfaces.AddRange(cleanedSurfaceSet);

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
            return;
        }

        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<PlayerData>(json);

        collectedSet = new HashSet<string>(data.collectedPickups);

        cleanedSurfaceSet = new HashSet<string>(data.cleanedSurfaces);

        seenMessagesSet = new HashSet<string>(data.seenMessages);

        CurrentLevelConfig = levelDatabase.Get(data.levelId);

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
        cleanedSurfaceSet.Clear();
        seenMessagesSet.Clear();
        data.seenMessages.Clear();

        DeleteAllCleanMasks();

        ApplylevelConfigSO("level_01");
    }

    private void DeleteAllCleanMasks()
    {
        string dir = Application.persistentDataPath;
        var files = Directory.GetFiles(dir, "cleanmask_*.png");

        foreach (var f in files)
            File.Delete(f);
    }

    public void MarkSurfaceCleaned(string surfaceId)
    {
        if (string.IsNullOrEmpty(surfaceId)) return;
        cleanedSurfaceSet.Add(surfaceId);
    }

    public bool IsSurfaceCleaned(string surfaceId)
    {
        return !string.IsNullOrEmpty(surfaceId) && cleanedSurfaceSet.Contains(surfaceId);
    }

    public void ApplylevelConfigSO(string levelId)
    {
        CurrentLevelConfig = levelDatabase.Get(levelId);
        if (CurrentLevelConfig == null) return;

        data.levelId = CurrentLevelConfig.levelId;
        data.day = CurrentLevelConfig.startingDay;
        data.time = CurrentLevelConfig.startingTime;

        PowerSytem.SetPower(CurrentLevelConfig.powerStartsOn);

        SaveProfile();
    }

    public bool HasSeenMessage(string messageId)
    {
        return !string.IsNullOrEmpty(messageId) && seenMessagesSet.Contains(messageId);
    }

    public void MarkMessageSeen(string messageId)
    {
        if (string.IsNullOrEmpty(messageId)) return;

        if (seenMessagesSet.Add(messageId))
        {
            data.seenMessages.Clear();
            data.seenMessages.AddRange(seenMessagesSet);
            SaveProfile();
        }
    }
}
