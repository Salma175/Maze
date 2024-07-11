using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDetails
{
    public int currentLevel;
    public int totalLevels;

    public GameDetails(int currentLevel, int totalLevels)
    {
        this.currentLevel = currentLevel;
        this.totalLevels = totalLevels;
    }
}

public interface IGameDetailsObserver
{
    void OnGameDataChanged(GameDetails newData);
}

public interface IGameDataManager
{
    GameDetails GetData();
    void SaveCurrentLevel(int level);

    void RegisterObserver(IGameDetailsObserver observer);
    void UnregisterObserver(IGameDetailsObserver observer);
}

public class GameDataManager : IGameDataManager
{
    private const string GameDetails = "GameDetails";
    private List<IGameDetailsObserver> observers = new List<IGameDetailsObserver>();

    public GameDetails GetData()
    {
        if (PlayerPrefs.HasKey(GameDetails))
        {
            string json = PlayerPrefs.GetString(GameDetails);

            return JsonUtility.FromJson<GameDetails>(json);
        }
        return new GameDetails(1,Constants.TotalLevels);
    }

    public void SaveCurrentLevel(int level)
    {
        var details = GetData();
        details.currentLevel = level;

        string json = JsonUtility.ToJson(details);

        PlayerPrefs.SetString(GameDetails, json);

        PlayerPrefs.Save();

        NotifyObservers(details);
    }

    public void RegisterObserver(IGameDetailsObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IGameDetailsObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers(GameDetails newDetails)
    {
        foreach (var observer in observers)
        {
            observer.OnGameDataChanged(newDetails);
        }
    }
}
