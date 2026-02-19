using UnityEngine;

public class Scratch : MonoBehaviour
{
    public float hitDistance = 1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RemoveRugPiece();
        }
    }

    private void RemoveRugPiece()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, hitDistance))
        {
            if (hit.collider.CompareTag("RugPiece"))
            {
                RugPiece piece = hit.collider.GetComponent<RugPiece>();
                if (piece != null)
                {
                    piece.Remove();
                }
            }
        }
    }
}
