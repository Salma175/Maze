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
}