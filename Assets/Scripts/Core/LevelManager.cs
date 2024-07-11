using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour, IGameDetailsObserver
{
    [SerializeField]
    private GameObject _noMoreLevelsGO;

    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private List<GameObject> levelObjects;

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

        GameEvents.OnRestartEvent += LoadDetails;

        ResetLevel();

        LoadDetails();
    }
    void OnDestroy()
    {
        GameEvents.OnRestartEvent -= LoadDetails;

        _gameDataManager.UnregisterObserver(this);
    }

    public void LoadDetails()
    {
        GameDetails details = _gameDataManager.GetData();

        LoadLevel(details.currentLevel);
    }


    // Method to load a level by name
    public void LoadLevel(int level)
    {
        levelObjects[level-1].gameObject.SetActive(true);

        ballTransform.position = startPositions[level-1].position;

        exitTransform.position = exitPositions[level-1].position;

        _levelText.text = $"Lv:{level}";
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

            _noMoreLevelsGO.SetActive(true);        
        }
    }

    public void OnGameDataChanged(GameDetails newData)
    {
        LoadLevel(newData.currentLevel);
    }

    private void ResetLevel()
    {
        LoadLevel(1);
    }
}
