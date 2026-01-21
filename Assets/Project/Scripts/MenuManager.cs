using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject optionsPanel;
    public GameObject collectionPanel;
    public GameObject creditsPanel;

    public GameObject audioPanel;

    public GameObject hud;

    private bool isPaused;

    private void Start()
    {
        Clear();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Menu();
        }

        else
        {
            hud.SetActive(true);   
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Clear()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        collectionPanel.SetActive(false);
        creditsPanel.SetActive(false);
        audioPanel.SetActive(false);
        hud.SetActive(false);
    }

    public void Menu()
    {
        optionsPanel.SetActive(false);
        collectionPanel.SetActive(false);
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void SinglePlayer()
    {
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
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            Clear();
            isPaused = !isPaused;
            hud.SetActive(!isPaused);
            menuPanel.SetActive(isPaused);

            if (isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

