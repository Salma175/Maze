public class PlayingState : IGameState
{
    public void EnterState()
    {
        GameEvents.ShowGameScreen();
    }

    public void ExitState()
    {
        GameEvents.HideGameScreen();
    }
}
