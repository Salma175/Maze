using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDetails
{
    public int currentLevel;
    public int totalLevels;
    public int score;

    public GameDetails(int currentLevel, int totalLevels, int score)
    {
        this.currentLevel = currentLevel;
        this.totalLevels = totalLevels;
        this.score = score;
    }
}

public interface IGameLevelObserver
{
    void OnGameLevelChanged(int level);
}

public interface IGameScoreObserver
{
    void OnGameScoreChanged(int score);
}

public interface IGameDataManager
{
    GameDetails GetData();
    void SaveCurrentLevel(int level);

    void RegisterObserver(IGameLevelObserver observer);
    void UnregisterObserver(IGameLevelObserver observer);

    void SaveCurrentScore(int score);

    void RegisterObserver(IGameScoreObserver observer);
    void UnregisterObserver(IGameScoreObserver observer);
}

public class GameDataManager : IGameDataManager
{
    private const string GameDetails = "GameDetails";
    private List<IGameLevelObserver> levelObservers = new List<IGameLevelObserver>();
    private List<IGameScoreObserver> scoreObservers = new List<IGameScoreObserver>();

    public GameDetails GetData()
    {
        if (PlayerPrefs.HasKey(GameDetails))
        {
            string json = PlayerPrefs.GetString(GameDetails);

            return JsonUtility.FromJson<GameDetails>(json);
        }
        return new GameDetails(1,Constants.TotalLevels,0);
    }

    public void SaveCurrentLevel(int level)
    {
        var details = GetData();
        details.currentLevel = level;

        string json = JsonUtility.ToJson(details);

        PlayerPrefs.SetString(GameDetails, json);

        PlayerPrefs.Save();

        NotifyLevelObservers(level);
    }

    public void RegisterObserver(IGameLevelObserver observer)
    {
        levelObservers.Add(observer);
    }

    public void UnregisterObserver(IGameLevelObserver observer)
    {
        levelObservers.Remove(observer);
    }

    private void NotifyLevelObservers(int level)
    {
        foreach (var observer in levelObservers)
        {
            observer.OnGameLevelChanged(level);
        }
    }

    public void SaveCurrentScore(int score)
    {
        var details = GetData();

        details.score = score;

        string json = JsonUtility.ToJson(details);

        PlayerPrefs.SetString(GameDetails, json);

        PlayerPrefs.Save();

        NotifyScoreObservers(score);
    }
    private void NotifyScoreObservers(int score)
    {
        foreach (var observer in scoreObservers)
        {
            observer.OnGameScoreChanged(score);
        }
    }
    public void RegisterObserver(IGameScoreObserver observer)
    {
        scoreObservers.Add(observer);
    }

    public void UnregisterObserver(IGameScoreObserver observer)
    {
        scoreObservers.Remove(observer);
    }
}
