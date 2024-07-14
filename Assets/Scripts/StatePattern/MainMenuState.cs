public class MainMenuState : IGameState
{
    public void EnterState()
    {
        GameEvents.ShowMainMenu();
    }

    public void ExitState()
    {
        GameEvents.HideMainMenu();
    }
}
