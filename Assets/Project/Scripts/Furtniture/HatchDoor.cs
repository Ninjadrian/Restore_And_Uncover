using UnityEngine;

public class HatchDoor : Door
{
    public override void Interact()
    {
        if (GameObject.FindGameObjectsWithTag("RugPiece").Length == 0)
        {
            OpenDoor();
        }
    }
}
