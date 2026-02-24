using UnityEngine;

public class Lantern : MonoBehaviour
{
    public Light lanternLight;
    private bool isTurnedOn = false;

    private void Awake()
    {
        lanternLight.enabled = false;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.PLAY)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SwitchLantern();
            }
        }    
    }

    private void SwitchLantern()
    {
        isTurnedOn = !isTurnedOn;
        lanternLight.enabled = isTurnedOn;
    }
}
