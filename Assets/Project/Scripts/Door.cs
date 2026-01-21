using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    public float duration = 1f;
    public Vector3 rotation;

    public AudioCollectionSO sfxCollection;

    private bool isOpen;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        StartCoroutine(MoveDoor());
    }

    public IEnumerator MoveDoor()
    {
        if (audioSource != null)
        {
            int numberClip = (isOpen ? 1 : 0);

            audioSource.PlayOneShot(sfxCollection.audioClips[numberClip]);
        }

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, 
                                                            this.transform.localRotation * Quaternion.Euler(rotation), 
                                                            Time.deltaTime);
            yield return null;
        }  

        isOpen = !isOpen;
        rotation = -rotation;
    }
}
