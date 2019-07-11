using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField] SoundAudioClip[] soundAudioClips = null;

    public static AudioManager Instance { get; private set; }

    AudioSource audioSource = null;

    public enum SoundType
    {
        Chasing,
        Background
    };

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }

    public void ChangeMusic(SoundType soundType)
    { 
        AudioClip targetClip = soundAudioClips.Where(o => o.soundType == soundType).Select(o => o.audioClip).First();

        if (targetClip != audioSource.clip)
        {
            audioSource.clip = targetClip;
            audioSource.Play();
        }
    }

    [System.Serializable]
    class SoundAudioClip
    {
        public SoundType soundType = SoundType.Background;
        public AudioClip audioClip = null;
    }
}