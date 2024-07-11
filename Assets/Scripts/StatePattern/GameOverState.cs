using UnityEngine;

public class GameOverState : IGameState
{
    public void EnterState()
    {
        // Initialize game over state
        Debug.Log("Entered Game Over State");
    }
    public void ExitState()
    {
        // Clean up game over state
        Debug.Log("Exited Game Over State");
    }
}
