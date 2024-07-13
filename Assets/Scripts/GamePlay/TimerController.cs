using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour, IGameDetailsObserver
{
    public TextMeshProUGUI timerText;
    private float timeRemaining;
    private bool isRunning = false;

    private IGameDataManager _gameDataManager;

    void Start()
    {
        _gameDataManager = ServiceLocator.Get<IGameDataManager>();

        _gameDataManager.RegisterObserver(this);

        timeRemaining = Constants.GameTime;

        isRunning = true;

        GameEvents.OnStartGame += ResetTimer;

        GameEvents.OnLevelCompleteEvent += StopTimer;

        GameEvents.OnPauseEvent += StopTimer;

        GameEvents.OnResumeEvent += StartTimer;

        GameEvents.OnRestartEvent += ResetTimer;
    }

    private void OnDestroy()
    {
        _gameDataManager.UnregisterObserver(this);

        GameEvents.OnStartGame -= ResetTimer;

        GameEvents.OnLevelCompleteEvent -= StopTimer;

        GameEvents.OnPauseEvent -= StopTimer;

        GameEvents.OnResumeEvent -= StartTimer;

        GameEvents.OnRestartEvent -= ResetTimer;
    }

    void Update()
    {
        if (isRunning)
        {
            UpdateTimer();
        }
    }

    void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Format and display the time
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            // Time has run out
            timeRemaining = 0;
            isRunning = false;
            timerText.text = "00:00";
            // Call a method to handle the timer reaching zero, if needed
            OnTimerEnd();
        }
    }

    private void StopTimer()
    {
        isRunning = false;
    }

    private void StartTimer()
    {
        isRunning = true;
    }

    public void ResetTimer()
    {
        timeRemaining = Constants.GameTime;
        isRunning = true;
    }

    private void OnTimerEnd()
    {
        GameEvents.LevelFail();

        AudioManager.Instance.PlaySFX(AudioClipName.LevelFail);
    }

    public void OnGameDataChanged(GameDetails newData)
    {
        ResetTimer();
    }
}
