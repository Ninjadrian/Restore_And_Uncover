using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] lights;
    public float offsetRotation;

    public bool hasKeyCard = true;

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
        if (hasKeyCard)
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
