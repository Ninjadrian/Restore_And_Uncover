using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState;

    public GameState gameState;

    private GameState stateBeforePause = GameState.Play;

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

    public void Pause()
    {
        if (gameState == GameState.Pause)
            return;

        stateBeforePause = gameState;
        gameState = GameState.Pause;

        Time.timeScale = 0f;

        UnlockCursor();
    }

    public void Resume()
    {
        gameState = stateBeforePause;
        Time.timeScale = 1f;

        if (gameState == GameState.Play || gameState == GameState.Puzzle)
        {
            LockCursor();
        }
        else
        {
            UnlockCursor();
        }
    }

    public void Play()
    {
        gameState = GameState.Play;
        Time.timeScale = 1f;

        LockCursor();
    }

    public void Puzzle()
    {
        gameState = GameState.Puzzle;
    }

    public void Fabrication()
    {
        gameState = GameState.Fabricate;
        UnlockCursor();
    }

    public void LevelCompleted()
    {
        Debug.Log("Nivel Completado");
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        //Desbloquear y mostrar cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

public enum GameState { Home, Play, Pause, Puzzle, Mobile, Fabricate, Inventory }
