using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mobile : MonoBehaviour
{
    public static Mobile Instance;

    public GameObject mobilePanel;
    public GameObject crosshairs;

    public GameObject mainMenuScreen;

    public GameObject introMessages;

    public float delaySeconds = 2.0f;

    public AudioSource audioSource;

    private bool isMobileActive;

    private void Awake()
    {
        Instance = this;

        ClearMessages();

        mainMenuScreen.SetActive(true);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delaySeconds);
        ShowMessages("msg_intro_level1", introMessages);
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.PLAY)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                MobileUI();
            }
        }
    }

    private void MobileUI()
    {
        isMobileActive = !isMobileActive;
        mobilePanel.SetActive(isMobileActive);
        crosshairs.SetActive(!isMobileActive);
    }

    private void ClearMessages()
    {
        mobilePanel.SetActive(false);
        introMessages.SetActive(false);
    }

    public void ShowMessages(string messageId, GameObject messagesPanel)
    {
        //Si ya se vio, ignorar
        if (PlayerProfiler.Instance != null && PlayerProfiler.Instance.HasSeenMessage(messageId))
            return;

        audioSource.Play();
        ClearMessages();
        messagesPanel.SetActive(true);

        PlayerProfiler.Instance?.MarkMessageSeen(messageId);
    }
}
    
