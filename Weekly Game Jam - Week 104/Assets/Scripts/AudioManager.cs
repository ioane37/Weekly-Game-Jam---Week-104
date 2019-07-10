using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    AudioSource audioSource = null;

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

    public void ChangeMusic(AudioClip newClip)
    {
        if (newClip != audioSource.clip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}