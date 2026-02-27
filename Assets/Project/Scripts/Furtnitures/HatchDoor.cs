using UnityEngine;

public class HatchDoor : MonoBehaviour
{
    private Door door;

    private void Start()
    {
        door = GetComponent<Door>();
    }

    public void TryOpenHatchDoor()
    {
        if (GameObject.FindGameObjectsWithTag("RugPiece").Length == 0)
        {
            door.OpenDoor();
        }
    }
}
