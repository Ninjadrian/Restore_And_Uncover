using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject singlePlayerPanel;
    public GameObject optionsPanel;
    public GameObject collectionPanel;
    public GameObject creditsPanel;

    public GameObject audioPanel;
    public GameObject videoPanel;

    public GameObject hud;

    public GameEvent pauseGameEvent;
    public GameEvent playGameEvent;

    private bool isPaused = false;

    private void Start()
    {
        Clear();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            menuPanel.SetActive(true);
        }

        else
        {
            hud.SetActive(true);   
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        } 

        if (Input.GetKeyDown(KeyCode.P))
        {
            hud.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            hud.SetActive(true);
        }
    }

    public void Clear()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        singlePlayerPanel.SetActive(false);
        collectionPanel.SetActive(false);
        creditsPanel.SetActive(false);

        audioPanel.SetActive(false);
        videoPanel.SetActive(false);

        hud.SetActive(false);
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
        GameManager.Instance.Play();
        SceneManager.LoadScene("Level1");
    }

    public void Cooperative()
    {

    }

    public void Options()
    {
        menuPanel.SetActive(false);
        audioPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void AudioOptions()
    {
        optionsPanel.SetActive(false);
        audioPanel.SetActive(true);
    }

    public void VideoOptions()
    {
        optionsPanel.SetActive(false);
        videoPanel.SetActive(true);
    }

    public void Collection()
    {
        menuPanel.SetActive(false);
        collectionPanel.SetActive(true);
    }

    public void Credits()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);  
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {     
        Clear();
        isPaused = !isPaused;
        hud.SetActive(!isPaused);
        menuPanel.SetActive(isPaused);

        if (isPaused)
        {
            pauseGameEvent.Raise();
        }
        else
        {
            playGameEvent.Raise();
        }
    }

    public void ReturnToMainMenu()
    {
        PlayerProfiler.Instance.SaveProfile();
        SceneManager.LoadScene("MainMenu");       
    }
}

