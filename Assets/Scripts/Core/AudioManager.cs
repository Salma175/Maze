using System;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClipName
{
    Button,
    LevelFail,
    LevelSuccess,
    Coin,
    Hit
}

[Serializable]
public struct AudioInfo
{
    public AudioClipName name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour, ISettingsObserver
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private List<AudioInfo> sfxClips;

    private IGameSettingsManager settingsManager;

    public static AudioManager Instance;

    private bool _canPlaySound = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        settingsManager = ServiceLocator.Get<IGameSettingsManager>();

        settingsManager.RegisterObserver(this);

        var settings = settingsManager.GetSettings();

        SetMusic(settings.music);

        _canPlaySound = settings.sound;
    }

    private void SetMusic(bool isOn)
    {
        if (isOn)
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }
    }

    void OnDestroy()
    {
        settingsManager.UnregisterObserver(this);
    }


    private void PlayMusic()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    private void StopMusic()
    {
        if(musicSource.isPlaying);
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(AudioClipName name)
    {
        AudioClip clip = sfxClips.Find(c => c.name == name).clip;

        if (_canPlaySound && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

   
    public void OnSettingsChanged(GameSettings newSettings)
    {
        SetMusic(newSettings.music);

        _canPlaySound = newSettings.sound;
    }
}
