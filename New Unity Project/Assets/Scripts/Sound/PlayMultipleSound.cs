
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayMultipleSound : MonoBehaviour
{

    [SerializeField]
    private TYPE_AUDIO[] typeAudio;

    [SerializeField]
    private bool isMusique = true;

    private SoundManager soundManager;
    private Sound[] soundsToPlay;
    private AudioSource audioSource;
    private TYPE_AUDIO typeAudioPlaying = TYPE_AUDIO.None;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        audioSource = GetComponent<AudioSource>();
        if (isMusique)
        {
            soundsToPlay = GetAllAudio(typeAudio, soundManager.soundsMusique);
        }
        else
        {
            soundsToPlay = GetAllAudio(typeAudio, soundManager.soundsSfx);
        }
        CheckForPlaySoundOnAwake();
    }

    private void Update()
    {
        if ((isMusique && !soundManager.activeMusic) || (!isMusique && !soundManager.activeSfx))
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
    }

    public Sound[] GetAllAudio(TYPE_AUDIO[] typeAudio, Sound[] allSounds)
    {
        Sound[] newListSound = new Sound[typeAudio.Length];
        int nombreAjout = 0;
        for (int i = 0; i < allSounds.Length; i++)
        {
            for (int j = 0; j < typeAudio.Length; j++)
            {
                if (allSounds[i].audioFor == typeAudio[j])
                {
                    newListSound[nombreAjout] = allSounds[i];
                    nombreAjout++;
                    break;
                }
            }
        }
        
        return newListSound;
    }

    public void PlaySound(TYPE_AUDIO typeAudio)
    {
        for (int i = 0; i < soundsToPlay.Length; i++)
        {
            if (soundsToPlay[i].audioFor == typeAudio)
            {
                audioSource.Stop();
                audioSource.clip = soundsToPlay[i].audio;
                audioSource.loop = soundsToPlay[i].loop;
                audioSource.volume = soundsToPlay[i].volume;
                audioSource.Play();
                typeAudioPlaying = soundsToPlay[i].audioFor;
                return;
            }
        }
    }

    public void CheckForPlaySoundOnAwake()
    {
        for (int i = 0; i < soundsToPlay.Length; i++)
        {
            if (soundsToPlay[i].playOnAwake && !audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.clip = soundsToPlay[i].audio;
                audioSource.loop = soundsToPlay[i].loop;
                audioSource.volume = soundsToPlay[i].volume;
                audioSource.Play();
                typeAudioPlaying = soundsToPlay[i].audioFor;
                return;
            }
        }
    }

    public TYPE_AUDIO GetEnumOfAudioPlaying()
    {
        return typeAudioPlaying;
    }

}
