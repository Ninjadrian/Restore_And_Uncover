using System.Collections;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public Vector3 localOffset = new Vector3(0f, 0f, 0.33f);
    public float duration = 1f;

    public void OpenDrawer()
    {
        StartCoroutine(MoveDrawer());
    }

    public IEnumerator MoveDrawer()
    {
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.transform.localPosition + localOffset, Time.deltaTime);
            yield return null;
        }

        localOffset = -localOffset;
    } 
}
