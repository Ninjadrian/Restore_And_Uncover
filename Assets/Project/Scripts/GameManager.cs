using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Play();

    }

    public void Pause()
    {
        gameState = GameState.PAUSE;
        Time.timeScale = 0f;
    }

    public void Play()
    {
        gameState = GameState.PLAY;
        Time.timeScale = 1f;
    }
}

public enum GameState { HOME, PLAY, PAUSE, Gameplay, Puzzle, Mobile }
