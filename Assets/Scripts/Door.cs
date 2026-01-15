using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float duration = 1f;
    public Vector3 rotation;

    public void OpenDoor()
    {
        StartCoroutine(MoveDoor());
    }

    public IEnumerator MoveDoor()
    {
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, 
                                                            this.transform.localRotation * Quaternion.Euler(rotation), 
                                                            Time.deltaTime);
            yield return null;
        }

        rotation = -rotation;
    }
}
