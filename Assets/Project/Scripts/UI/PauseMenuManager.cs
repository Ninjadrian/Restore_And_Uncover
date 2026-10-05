using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance { get; private set; }

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject collectionPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Option Panels")]
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject videoPanel;
    [SerializeField] private GameObject controlPanel;

    private bool isPaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    private void Clear()
    {
        pausePanel?.SetActive(false);
        optionsPanel?.SetActive(false);
        collectionPanel?.SetActive(false);
        creditsPanel?.SetActive(false);

        audioPanel?.SetActive(false);
        videoPanel?.SetActive(false);
        controlPanel?.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;

        Clear();

        GameManager.Instance.Resume();
    }

    public void PauseGame()
    {
        isPaused = true;

        Menu();

        GameManager.Instance.Pause();
    }

    public void Menu()
    {
        Clear();
        pausePanel.SetActive(true);
    }

    public void Options()
    {
        Clear();
        optionsPanel.SetActive(true);
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

    public void ReturnToMainMenu()
    {
        PlayerProfiler.Instance?.SaveProfile();

        isPaused = false;

        GameManager.Instance.Home();

        Instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene("MainMenu");
    }


    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
