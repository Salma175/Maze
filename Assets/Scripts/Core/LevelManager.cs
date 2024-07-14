using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour, IGameLevelObserver
{
    [SerializeField]
    private GameObject _noMoreLevelsGO;

    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private List<GameObject> levelObjects;

    [SerializeField]
    private List<GameObject> collectables;

    [SerializeField]
    private List<Transform> startPositions;

    [SerializeField]
    private List<Transform> exitPositions;

    [SerializeField]
    private Transform ballTransform;

    [SerializeField]
    private Transform exitTransform;

    private IGameDataManager _gameDataManager;
   
    void Start()
    {
        _gameDataManager = ServiceLocator.Get<IGameDataManager>();

        _gameDataManager.RegisterObserver(this);

        GameEvents.OnStartGame += LoadDetails;

        GameEvents.OnRestartEvent += LoadDetails;

        GameEvents.OnLevelCompleteEvent += ResetBall;

        ResetLevel();

        LoadDetails();
    }
    void OnDestroy()
    {
        GameEvents.OnStartGame -= LoadDetails;

        GameEvents.OnLevelCompleteEvent -= ResetBall;

        GameEvents.OnRestartEvent -= LoadDetails;

        _gameDataManager.UnregisterObserver(this);
    }

    public void LoadDetails()
    {
        GameDetails details = _gameDataManager.GetData();

        LoadLevel(details.currentLevel);
    }

    private void ResetBall()
    {
        int index = _gameDataManager.GetData().currentLevel - 1;

        ballTransform.CopyTransformFrom(startPositions[index]);
    }

    // Method to load a level by name
    public void LoadLevel(int level)
    {
        ResetLevel();

        int index = level - 1;

        levelObjects[index].gameObject.SetActive(true);

        collectables[index].gameObject.SetActive(true);

        ballTransform.CopyTransformFrom(startPositions[index]);

        exitTransform.CopyTransformFrom(exitPositions[index]);

        _levelText.text = $"Lv: {level}";
    }


    // Method to load the next level
    public void LoadNextLevel()
    {
        GameDetails details = _gameDataManager.GetData();

        int currentLevel = details.currentLevel + 1;

        if (currentLevel <= details.totalLevels)
        {
            _gameDataManager.SaveCurrentLevel(currentLevel);
        }
        else
        {
            _gameDataManager.SaveCurrentLevel(1);

            _gameDataManager.SaveCurrentScore(0);

            _noMoreLevelsGO.SetActive(true);        
        }
    }

    public void OnGameLevelChanged(int level)
    {
        LoadLevel(level);
    }

    private void ResetLevel()
    {
        foreach (var item in levelObjects)
        {
            item.gameObject.SetActive(false);  
        }
        foreach (var item in collectables)
        {
            item.gameObject.SetActive(false);
        }
        ballTransform.CopyTransformFrom(startPositions[0]);

        exitTransform.CopyTransformFrom(exitPositions[0]);
    }
}
