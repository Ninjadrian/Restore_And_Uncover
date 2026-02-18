using UnityEngine;
using UnityEngine.UI;

public class BrightnessSettings : MonoBehaviour
{
    public Image overlayImage;

    public void ChangeAlpha(float alpha)
    {
        Color c = overlayImage.color;
        c.a = alpha;
        overlayImage.color = c;
    }
}
