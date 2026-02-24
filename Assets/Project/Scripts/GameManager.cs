using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState;

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
    }

    public void Pause()
    {
        gameState = GameState.PAUSE;
        Time.timeScale = 0f;

        UnlockCursor();
    }

    public void Play()
    {
        gameState = GameState.PLAY;
        Time.timeScale = 1f;

        // Bloquear y esconder el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        Debug.Log("Nivel Completdo");
    }

    public void UnlockCursor()
    {
        //Desbloquear y mostrar cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

public enum GameState { HOME, PLAY, PAUSE, Puzzle, Mobile, Fabricate, Inventory }
