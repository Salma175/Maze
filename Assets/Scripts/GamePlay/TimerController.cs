using TMPro;
using UnityEngine;
using DG.Tweening;

public class TimerController : MonoBehaviour, IGameDetailsObserver
{
    [SerializeField]
    private Transform timerTransform;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private AudioSource heartbeatAudioSource;

    private float heartbeatInterval = 0.5f;
    private float timeRemaining;
    private bool isRunning = false;
    private bool heartbeatStarted = false;

    private IGameDataManager _gameDataManager;
    private Tween timerTween;

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

            if (timeRemaining <= 10f && !heartbeatStarted)
            {
                StartHeartbeat();
            }
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

        StopHeartbeat();
    }

    private void StartTimer()
    {
        isRunning = true;
    }

    public void ResetTimer()
    {
        timeRemaining = Constants.GameTime;

        isRunning = true;

        StopHeartbeat();
    }

    private void OnTimerEnd()
    {
        StopHeartbeat();

        GameEvents.LevelFail();

        AudioManager.Instance.PlaySFX(AudioClipName.LevelFail);
    }

    public void OnGameDataChanged(GameDetails newData)
    {
        ResetTimer();
    }

    private void StartHeartbeat()
    {
        heartbeatStarted = true;
       
        timerTween = timerTransform.DOScale(1.2f, heartbeatInterval).SetLoops(-1, LoopType.Yoyo);
      
        heartbeatAudioSource.Play();
    }

    private void StopHeartbeat()
    {
        heartbeatStarted = false;

        if (timerTween != null && timerTween.IsActive())
        {
            timerTween.Kill();
            timerTransform.localScale = Vector3.one; 
        }
        heartbeatAudioSource.Stop();
    }
}
