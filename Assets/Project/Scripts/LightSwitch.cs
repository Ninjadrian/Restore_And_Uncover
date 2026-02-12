using Unity.VisualScripting;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] lights;
    public float offsetRotation;

    private bool hasPower;

    private void OnEnable()
    {
        PowerSytem.OnPowerChanged += HandlePowerChanged;

        hasPower = PowerSytem.HasPower;
    }

    private void OnDisable()
    {
        PowerSytem.OnPowerChanged -= HandlePowerChanged;
    }

    private void HandlePowerChanged(bool value)
    {
        hasPower = value;
    }

    public void SwitchLights()
    {
        if (hasPower)
        {
            foreach (Light light in lights)
            {
                light.enabled = !light.enabled;
            }
        }

        Transform sw = transform.GetChild(1);
        sw.rotation = sw.rotation * Quaternion.Euler(offsetRotation, 0f, 0f);
        offsetRotation = -offsetRotation;
    }
}
