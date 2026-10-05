using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject singlePlayerPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject collectionPanel;
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject videoPanel;
    [SerializeField] private GameObject controlPanel;

    private void Start()
    {
        Clear();

        menuPanel.SetActive(true);        
    }

    public void Clear()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        singlePlayerPanel.SetActive(false);
        collectionPanel.SetActive(false);
        creditsPanel.SetActive(false);
        
        controlPanel.SetActive(false);
        audioPanel.SetActive(false);
        videoPanel.SetActive(false);
    }

    public void Menu()
    {
        Clear();
        menuPanel.SetActive(true);
    }

    public void SinglePlayer()
    {
        menuPanel.SetActive(false);
        singlePlayerPanel.SetActive(true);
    }

    public void StartSingleGame()
    {
        PlayerProfiler.Instance.StartNewGame();
        InventoryManager.Instance.InitializeInventory();

        GameManager.Instance.Play();
        SceneManager.LoadScene("Level1");
    }

    public void ContinueSingleGame()
    {
        PlayerProfiler.Instance.LoadProfile();

        LevelConfigSO levelConfig = PlayerProfiler.Instance.CurrentLevelConfig;

        if (levelConfig == null || string.IsNullOrEmpty(levelConfig.sceneName))
        {
            return;
        }

        GameManager.Instance.Play();
        SceneManager.LoadScene(levelConfig.sceneName);
    }

    public void Cooperative()
    {

    }

    public void Options()
    {
        Clear();
        optionsPanel.SetActive(true);
    }

    public void AudioOptions()
    {
        Clear();
        audioPanel.SetActive(true);
    }

    public void VideoOptions()
    {
        Clear();
        videoPanel.SetActive(true);
    }

    public void ControlOptions()
    {
        Clear();
        controlPanel.SetActive(true);
    }

    public void Collection()
    {
        Clear();
        collectionPanel.SetActive(true);
    }

    public void Credits()
    {
        Clear();
        creditsPanel.SetActive(true);  
    }

    public void Exit()
    {
        Application.Quit();
    }
}

