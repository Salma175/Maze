using UnityEngine;

public class UIManager : MonoBehaviour
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
        GameEvents.OnShowMainMenuEvent += MainMenu;
        GameEvents.OnHideMainMenuEvent += () => { HidePanel(_mainscreen); };

        GameEvents.OnStartGame += StartGame;
        GameEvents.OnEndGame += EndGame;

        GameEvents.OnLevelFailEvent += LevelFail;

        GameEvents.OnLevelCompleteEvent += LevelPass;

        #endregion
    }

    private void OnDestroy()
    {
        GameEvents.OnShowMainMenuEvent -= MainMenu;
        GameEvents.OnStartGame -= StartGame;
        GameEvents.OnEndGame -= EndGame;
        GameEvents.OnLevelCompleteEvent -= LevelPass;
        GameEvents.OnLevelFailEvent -= LevelFail;
    }
    private void MainMenu()
    {
        ShowPanel(_mainscreen);
    }
    private void LevelPass()
    {
        ShowPanel(_levelPassScreen);
    }
    private void LevelFail()
    {
        ShowPanel(_levelFailScreen);
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
