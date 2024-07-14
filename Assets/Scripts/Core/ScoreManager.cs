using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour, IGameScoreObserver
{
    [SerializeField]
    private TextMeshProUGUI _score;

    private IGameDataManager _gameDataManager;

    private void Start()
    {
        _gameDataManager = ServiceLocator.Get<IGameDataManager>();

        _gameDataManager.RegisterObserver(this);

        _score.text = _gameDataManager.GetData().score.ToString();
    }

    private void OnDestroy()
    {
        _gameDataManager.UnregisterObserver(this);
    }
    public void OnGameScoreChanged(int score)
    {
        _score.text = score.ToString();
    }

}
