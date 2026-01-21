using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Audio;

public class Drawer : MonoBehaviour
{
    public Vector3 localOffset = new Vector3(0f, 0f, 0.33f);
    public float duration = 1f;

    public AudioCollectionSO sfxCollection;

    private bool isOpen;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenDrawer()
    {
        StartCoroutine(MoveDrawer());
    }

    public IEnumerator MoveDrawer()
    {
        if (!isOpen)
        {
            audioSource.PlayOneShot(sfxCollection.audioClips[0]);
        }
        else
        {
            audioSource.PlayOneShot(sfxCollection.audioClips[1]);
        }

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.transform.localPosition + localOffset, Time.deltaTime);
            yield return null;
        }

        isOpen = !isOpen;
        localOffset = -localOffset;
    } 
}
