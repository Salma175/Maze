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
    public static event Action OnShowGameScreenEvent;

    public static event Action OnHideGameScreenEvent;

    public static void ShowGameScreen()
    {
        OnShowGameScreenEvent?.Invoke();
    }

    public static void HideGameScreen()
    {
        OnHideGameScreenEvent?.Invoke();
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