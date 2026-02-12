using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEngine.AudioSettings;

public class MobileUI : MonoBehaviour
{
    public GameObject mobilePanel;
    public GameObject crosshairs;

    public GameObject mainMenuScreen;
    public GameObject messajesScreen;

    private bool isMobileActive;

    private void Awake()
    {
        mobilePanel.SetActive(false);
        messajesScreen.SetActive(false);

        mainMenuScreen.SetActive(true);
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.PLAY)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Mobile();
            }
        }
    }

    public void Mobile()
    {        
        isMobileActive = !isMobileActive;
        mobilePanel.SetActive(isMobileActive);  
        crosshairs.SetActive(!isMobileActive);
    }

    public void MessagesScreen()
    {
        messajesScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }
}
