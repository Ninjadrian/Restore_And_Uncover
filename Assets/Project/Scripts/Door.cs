using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    [SerializeField] private string keyId;

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
        if (string.IsNullOrEmpty(keyId))
        {
            MoveDoor();
        }
        else 
        {
            ToolData activeTool = ToolRig.Instance.GetCurrentTool();
            if (activeTool.id == keyId)
            {
                MoveDoor();
                keyId = null;
                InventoryManager.Instance.RemoveTool(activeTool);
                ToolRig.Instance.Unequip();
            }
            else return;
        }
    }

    public void MoveDoor()
    {
        StartCoroutine(DoorAnimation());
    }

    private IEnumerator DoorAnimation()
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
