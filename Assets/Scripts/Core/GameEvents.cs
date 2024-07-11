using System;

public static class GameEvents
{
    #region Main Menu

    public static event Action OnShowMainMenuEvent;

    public static event Action OnHideMainMenuEvent;

    public static void ShowMainMenu()
    {
        OnShowMainMenuEvent?.Invoke();
    }

    public static void HideMainMenu()
    {
        OnHideMainMenuEvent?.Invoke();
    }
    #endregion

    #region Game Playing State
    public static event Action OnStartGame;

    public static event Action OnEndGame;

    public static void ShowGameScreen()
    {
        OnStartGame?.Invoke();
    }

    public static void HideGameScreen()
    {
        OnEndGame?.Invoke();
    }
    #endregion


    #region Game Pause
    public static event Action OnPauseEvent;

    public static event Action OnResumeEvent;

    public static void PauseGame()
    {
        OnPauseEvent?.Invoke();
    }

    public static void ResumeGame()
    {
        OnResumeEvent?.Invoke();
    }

    public static event Action OnRestartEvent;

    public static void RestartGame()
    {
        OnRestartEvent?.Invoke();
    }

    #endregion

    #region Level Fail/Success

    public static event Action OnLevelFailEvent;

    public static void LevelFail()
    {
        OnLevelFailEvent?.Invoke();
    }


    public static event Action OnLevelCompleteEvent;

    public static void LevelComplete()
    {
        OnLevelCompleteEvent?.Invoke();
    }
    #endregion
}