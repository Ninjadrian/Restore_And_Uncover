using UnityEngine;

public class Strongbox : CodePuzzleBase
{
    private Door door;
    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        door = GetComponentInChildren<Door>();
    }

    protected override void OnCodeCorrect()
    {
        if (door != null)
        {
            door.MoveDoor();
        }

        if (boxCollider != null) 
        {
            boxCollider.enabled = false;
        }
    }
}
