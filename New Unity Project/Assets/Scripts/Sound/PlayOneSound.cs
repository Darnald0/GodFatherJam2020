using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayOneSound : MonoBehaviour
{
    [HideInInspector]
    public bool declencherAudio = false;

    [SerializeField]
    private bool isMusique = true;

    [SerializeField]
    private TYPE_AUDIO typeAudio;

    private SoundManager soundManager;
    private Sound soundToPlay;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        audioSource = GetComponent<AudioSource>();

        if (isMusique)
        {
            soundToPlay = GetAudio(typeAudio, soundManager.soundsMusique);
        }
        else
        {
            soundToPlay = GetAudio(typeAudio, soundManager.soundsSfx);
        }

        if (soundToPlay != null)
        {
            audioSource.clip = soundToPlay.audio;
            audioSource.loop = soundToPlay.loop;
            audioSource.volume = soundToPlay.volume;
            if (soundToPlay.playOnAwake)
            {
                PlaySound();
            }
        }
        else
        {
            Debug.LogError("AUCUN AUDIO POUR CE TYPE D'AUDIO REFERENCER DANS SOUNDMANAGER");
        }

    }

    private void Update()
    {
        if ( (isMusique && !soundManager.activeMusic) || (!isMusique && !soundManager.activeSfx) )
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
    }

    public Sound GetAudio(TYPE_AUDIO typeAudio, Sound[] allSounds)
    {
        foreach (Sound soundSelected in allSounds)
        {
            if (soundSelected.audioFor == typeAudio)
            {
                return soundSelected;
            }
        }
        return null;
    }

    public void PlaySound()
    {
        audioSource.Play();
    }   
    public void StopSound()
    {
        audioSource.Stop();
    }

}
