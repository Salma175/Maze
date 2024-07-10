using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : IGameSettingsManager
{
    private const string SettingsKey = "GameSettings";
    private List<ISettingsObserver> observers = new List<ISettingsObserver>();

    public GameSettings GetSettings()
    {
        if (PlayerPrefs.HasKey(SettingsKey))
        {
            string json = PlayerPrefs.GetString(SettingsKey);

            return JsonUtility.FromJson<GameSettings>(json);
        }
        return new GameSettings(); 
    }

    public void SaveSettings(GameSettings settings)
    {
        string json = JsonUtility.ToJson(settings);

        PlayerPrefs.SetString(SettingsKey, json);

        PlayerPrefs.Save();

        NotifyObservers(settings);
    }

    public void RegisterObserver(ISettingsObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(ISettingsObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers(GameSettings newSettings)
    {
        foreach (var observer in observers)
        {
            observer.OnSettingsChanged(newSettings);
        }
    }
}
