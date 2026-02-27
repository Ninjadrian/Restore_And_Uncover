using UnityEngine;

public class OneShotMessageTrigger : MonoBehaviour
{
    public string messageId;
    public GameObject messagePanel;

    private void Start()
    {
        if (PlayerProfiler.Instance != null && PlayerProfiler.Instance.HasSeenMessage(messageId))
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Mobile.Instance.ShowMessages(messageId, messagePanel);
        Destroy(gameObject);
    }
}
