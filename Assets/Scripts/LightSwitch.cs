using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] lights;

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
    }
}
