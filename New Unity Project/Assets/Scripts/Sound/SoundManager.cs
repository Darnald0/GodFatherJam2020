using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    public Sound[] soundsMusique;
    public Sound[] soundsSfx;

    [HideInInspector]
    public bool activeMusic = true;
    [HideInInspector]
    public bool activeSfx = true;

    [SerializeField]
    private AudioMixer mainMixerAudio;

    [SerializeField]
    private Slider sliderVolumeMusique;
    [SerializeField]
    private Slider sliderVolumeSfx;

    public void SwitchActiveMusique()
    {
       activeMusic = !activeMusic;
    }

    public void SetVolumeMusique()
    {
        mainMixerAudio.SetFloat("VolumeMusique", sliderVolumeMusique.value);
    }

    public void SwitchActiveSfx()
    {
        activeSfx = !activeSfx;
    }

    public void SetVolumeSfx()
    {
        mainMixerAudio.SetFloat("VolumeSfx", sliderVolumeSfx.value);
    }
}
