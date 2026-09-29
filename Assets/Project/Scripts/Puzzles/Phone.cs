using UnityEngine;

public class Phone : CodePuzzleBase
{
    [SerializeField] private GameObject BluePrint;

    protected override void OnCodeCorrect()
    {
        BluePrint.SetActive(true);
    }
}
