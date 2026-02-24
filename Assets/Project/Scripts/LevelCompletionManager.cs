using UnityEngine;
using System.Collections.Generic;

public class LevelCompletionManager : MonoBehaviour
{
    public static LevelCompletionManager Instance;

    [SerializeField] private string requiredItemId;

    private bool hasItem;
    private bool levelCompleted;

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

    public void TryCompleteLevel()
    {
        if (levelCompleted) return;

        bool recyclablesDone = CheckRecyclables();
        bool surfacesDone = CheckSurfaces();

        if (hasItem && recyclablesDone && surfacesDone)
        {
            CompleteLevel();
        }
    }

    public void CheckRequiredItem(string itemID)
    {
        if (itemID == requiredItemId)
        {
            //Debug.Log("Objetivo de item completado");
            TryCompleteLevel();
            hasItem = true;
        }
    }

    private bool CheckRecyclables()
    {
        return FindObjectsByType<RecyclablePickUp>(FindObjectsSortMode.None).Length == 0;
    }

    private bool CheckSurfaces()
    {
        return FindObjectsByType<CleanableSurface>(FindObjectsSortMode.None).Length == 0;
    }

    private void CompleteLevel()
    {
        levelCompleted = true;
        Debug.Log("Nivel Completado");
    }
}
