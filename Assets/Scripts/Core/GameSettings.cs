[System.Serializable]
public class GameSettings
{
    public bool music;
    public bool sound;
    public GameSettings()
    {
        music = true; sound = true;
    }
}

public interface ISettingsObserver
{
    void OnSettingsChanged(GameSettings newSettings);
}

public interface IGameSettingsManager
{
    GameSettings GetSettings();

    void SaveSettings(GameSettings settings);

    void RegisterObserver(ISettingsObserver observer);
    void UnregisterObserver(ISettingsObserver observer);
}


