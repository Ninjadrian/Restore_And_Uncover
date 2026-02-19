using UnityEngine;

public class Lantern : MonoBehaviour
{
    public Light lanternLight;
    private bool isTurnedOn = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwitchLantern();
        }
    }

    private void SwitchLantern()
    {
        isTurnedOn = !isTurnedOn;
        lanternLight.enabled = isTurnedOn;
    }
}
