using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class MessagesUI : MonoBehaviour
{
    public float delaySeconds = 2.0f;

    [SerializeField] private GameObject[] messagesText;
    [SerializeField] private TMP_Text unreadBadgeText;
    [SerializeField] private GameObject unreadBadgeRoot;


    public void MessageReceived(int messageIndex)
    {
        messagesText[messageIndex].SetActive(true);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delaySeconds);
        MessageReceived(0);
        //Debug.Log("Mensaje recibido");
    }

    //private void OnEnable()
    //{
    //    StartCoroutine(WaitAndSuscribe());  
    //}

    //private IEnumerator WaitAndSuscribe()
    //{
    //    while (Messages.Instance == null)
    //        yield return null;

    //    Messages.Instance.OnChanged += Refresh;
    //    Refresh();
    //}

    //private void OnDisable()
    //{
    //    if (Messages.Instance != null)
    //        Messages.Instance.OnChanged -= Refresh;
    //}

    //public void Refresh()
    //{
    //    messageText.text = Messages.Instance.GetLatestOrEmpty();

    //    int unread = Messages.Instance.GetUnreadCount();
    //    unreadBadgeRoot.SetActive(unread > 0);
    //    unreadBadgeText.text = unread.ToString();
    //}

    //public void OnOpenMessagesApp()
    //{
    //    Messages.Instance.MarkAllAsRead();
    //}
}
