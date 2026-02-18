using UnityEngine;

[CreateAssetMenu(fileName = "AudioCollectionSO", menuName = "Scriptable Objects/AudioCollectionSO")]
public class AudioCollectionSO : ScriptableObject
{
    public AudioClip[] audioClips;
}
