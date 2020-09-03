using UnityEngine;

public enum TYPE_AUDIO
{
    None,
    MusicDay,
    MusicNight,
    MusicMenu,
    MusicGameOver,
    MusicWin,
    Malus,
    Bonus,
    GetWood,
    CuttingTree,
    BreakBarricade,
    RunningLeave,
    WalkLeave,
    BuildHouse,
}

[System.Serializable]
public class Sound
{
    public TYPE_AUDIO audioFor;
    public AudioClip audio;
    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
    public bool loop;
    public bool playOnAwake;
}