using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("List containing all SFXs INDEX:" +
        "0 -> Footstep" +
        "1 -> Possesion" +
        "2 -> Interaction" +
        "3 -> Death" +
        "4 -> PLACEHOLDER")]
    [SerializeField] AudioClip[] SFX_Clips;
    [Tooltip("List containing all Music ")]
    [SerializeField] AudioClip[] Music_Clips;
    [SerializeField] AudioSource SFX_AudioSource;
    [SerializeField] AudioSource Music_AudioSource;
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
    }

    public void PlaySFX(int AudioClipID)
    {
        if (AudioClipID > SFX_Clips.Length - 1) 
        {
            Debug.LogAssertion("AudioClipID is out of range of the audioClips array.");
        }
        // Implementation for playing sound
        SFX_AudioSource.PlayOneShot(SFX_Clips[AudioClipID]);
        Debug.Log($"Playing sound with the following index: {AudioClipID}");
    }

    public void PlayMusic(int MusicClipID)
    {
        if (MusicClipID > Music_Clips.Length - 1)
        {
            Debug.LogAssertion("AudioClipID is out of range of the audioClips array.");
        }
        Debug.Log($"Playing sound with the following index: {MusicClipID}");
        Music_AudioSource.Stop();
        Music_AudioSource.PlayOneShot(Music_Clips[MusicClipID]);
    }
}
