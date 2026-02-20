using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePickup : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyPickupState();
    }

    private void ApplyPickupState()
    {
        UniquePickupId[] pickups = FindObjectsByType<UniquePickupId>(
            FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var p in pickups)
        {
            if (PlayerProfiler.Instance != null && PlayerProfiler.Instance.IsPickUpCollected(p.id))
            {
                p.gameObject.SetActive(false);
            }
        }
    }
}
