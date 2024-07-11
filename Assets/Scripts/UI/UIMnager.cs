using UnityEngine;

public class UIMnager : MonoBehaviour
{
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private GameObject _mainscreen;
    [SerializeField]
    private GameObject _infoScreen;
    [SerializeField]
    private GameObject _levelFailScreen;
    [SerializeField]
    private GameObject _levelPassScreen;
    [SerializeField]
    private GameObject _gameScreen;

    private CommandInvoker invoker;

    void Awake()
    {
        invoker = new CommandInvoker();

        #region Events
        GameEvents.OnShowMainMenuEvent += ()=> { ShowPanel(_mainscreen); };
        GameEvents.OnHideMainMenuEvent += () => { HidePanel(_mainscreen); };

        GameEvents.OnShowGameScreenEvent += () => { ShowPanel(_gameScreen); };
        GameEvents.OnHideGameScreenEvent += () => { HidePanel(_gameScreen); };

        GameEvents.OnLevelFailEvent += () => { ShowPanel(_levelFailScreen); };

        GameEvents.OnLevelCompleteEvent += () => { ShowPanel(_levelPassScreen); };

        #endregion
    }

    public void PlayGame()
    {
        GameStateManager.Instance.ChangeState(new PlayingState());
    }

    public void ShowInfo()
    {
        ShowPanel(_infoScreen);
    }

    public void HideInfo()
    {
        HidePanel(_infoScreen);
    }

    public void Home()
    {
        GameStateManager.Instance.ChangeState(new MainMenuState());
    }

    public void Pause()
    {
        GameEvents.PauseGame();
    }

    public void Resume()
    {
        GameEvents.ResumeGame();
    }

    public void Restart()
    {
        GameEvents.RestartGame();
    }

    private void ShowPanel(GameObject panel)
    {
        ICommand showPanelCommand = new ShowPanelCommand(panel);
        invoker.SetCommand(showPanelCommand);
        invoker.ExecuteCommand();
    }

    private void HidePanel(GameObject panel) 
    {
        ICommand hidePanelCommand = new HidePanelCommand(panel);
        invoker.SetCommand(hidePanelCommand);
        invoker.ExecuteCommand();
    }

}
