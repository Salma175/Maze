using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour, ISettingsObserver
{
    [Header("Music")]
    [SerializeField]
    public Image music;
    [SerializeField]
    public Sprite musicOnIcon;
    [SerializeField]
    public Sprite musicOffIcon;

    [Space]
    [Header("Sound")]
    [SerializeField]
    public Image sound;
    [SerializeField]
    public Sprite soundOnIcon;
    [SerializeField]
    public Sprite soundOffIcon;


    private IGameSettingsManager settingsManager;

    void Start()
    {
        settingsManager = ServiceLocator.Get<IGameSettingsManager>();

        settingsManager.RegisterObserver(this);

        LoadSettings();
    }

    void OnDestroy()
    {
        settingsManager.UnregisterObserver(this);
    }

    public void LoadSettings()
    {
        GameSettings settings = settingsManager.GetSettings();

        music.overrideSprite = settings.music? musicOnIcon: musicOffIcon;

        sound.overrideSprite = settings.sound ? soundOnIcon : soundOffIcon;
    }

    public void SaveMusicSettings()
    {
        GameSettings settings = settingsManager.GetSettings();

        settings.music = !settings.music;

        settingsManager.SaveSettings(settings);
    }

    public void SaveSoundSettings()
    {
        GameSettings settings = settingsManager.GetSettings();

        settings.sound = !settings.sound;

        settingsManager.SaveSettings(settings);
    }

    public void OnSettingsChanged(GameSettings newSettings)
    {
        music.overrideSprite = newSettings.music ? musicOnIcon : musicOffIcon;

        sound.overrideSprite = newSettings.sound ? soundOnIcon : soundOffIcon;
    }
}
