using UnityEngine;

public class UIMnager : MonoBehaviour
{
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

    [SerializeField]
    private GameObject _gamePlayGO;

    private CommandInvoker invoker;

    void Awake()
    {
        invoker = new CommandInvoker();

        #region Events
        GameEvents.OnShowMainMenuEvent += ()=> { ShowPanel(_mainscreen); };
        GameEvents.OnHideMainMenuEvent += () => { HidePanel(_mainscreen); };

        GameEvents.OnStartGame += StartGame;
        GameEvents.OnEndGame += EndGame;

        GameEvents.OnLevelFailEvent += () => { ShowPanel(_levelFailScreen); };

        GameEvents.OnLevelCompleteEvent += () => { ShowPanel(_levelPassScreen); };

        #endregion
    }

    private void OnDestroy()
    {
        GameEvents.OnStartGame -= StartGame;
        GameEvents.OnEndGame -= EndGame;
    }

    private void StartGame()
    {
        ShowPanel(_gameScreen);
        ShowPanel(_gamePlayGO);
    }

    private void EndGame()
    {
        HidePanel(_gameScreen);
        HidePanel(_gamePlayGO);
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
