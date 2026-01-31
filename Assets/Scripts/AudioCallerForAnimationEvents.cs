using UnityEngine;

public class AudioCallerForAnimationEvents : MonoBehaviour
{
    public void PlayAudioClipByID(int audioClipID)
    {
        AudioManager.Instance.PlaySFX(audioClipID);
    }
}
