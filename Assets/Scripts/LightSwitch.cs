using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] lights;
    public float offsetRotation;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    SwitchLights();
        //}
    }

    public void SwitchLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = !light.enabled;
        }

        Transform sw = transform.GetChild(1);
        sw.rotation = sw.rotation * Quaternion.Euler(offsetRotation, 0f, 0f);
        offsetRotation = -offsetRotation;
    }
}
