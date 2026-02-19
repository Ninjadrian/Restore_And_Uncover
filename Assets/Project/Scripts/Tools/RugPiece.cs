using UnityEngine;

public class RugPiece : MonoBehaviour
{
    public string pieceId;

    private void Start()
    {
        if (PlayerProfiler.Instance.IsPickUpCollected(pieceId))
        {
            Destroy(gameObject);
        }
    }

    public void Remove()
    {
        PlayerProfiler.Instance.MarkPickupCollected(pieceId);
        PlayerProfiler.Instance.SaveProfile();
        Destroy(gameObject);
    }
}
