using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelCompletionManager : MonoBehaviour
{
    public static LevelCompletionManager Instance;

    public GameObject levelCompletedPanel;
    public GameObject hud;

    [SerializeField] private string requiredItemId;

    [SerializeField] private string nextLevelId;

    private bool hasItem;
    private bool levelCompleted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

        if(hasItem) Debug.Log("Objetivo de item completado");
        if (recyclablesDone) Debug.Log("Se recogieron todos los reciclables");
        if (surfacesDone) Debug.Log("Se limpiaron todas las superficies");

        if (hasItem && recyclablesDone && surfacesDone)
        {
            CompleteLevel();
        }
    }

    public void CheckRequiredItem(string itemID)
    {
        if (itemID == requiredItemId)
        {
            hasItem = true;
            TryCompleteLevel();
        }
    }

    private bool CheckRecyclables()
    {
        return FindObjectsByType<RecyclablePickUp>(FindObjectsSortMode.None).Length <= 1;
    }

    private bool CheckSurfaces()
    {
        return FindObjectsByType<CleanableSurface>(FindObjectsSortMode.None).Length == 0;
    }

    private void CompleteLevel()
    {
        levelCompletedPanel.SetActive(true);
        hud.SetActive(false);
        levelCompleted = true;

        GameManager.Instance.Pause();
        if (string.IsNullOrEmpty(nextLevelId)) return;

        PlayerProfiler.Instance.ApplylevelConfigSO(nextLevelId);
    }

    public void SecondLevel()
    {
        GameManager.Instance.Play();
        SceneManager.LoadScene("Level2");
    }
}
