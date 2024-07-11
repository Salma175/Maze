using UnityEngine;

public class PausedState : IGameState
{
    public void EnterState()
    {
        // Initialize paused state
        Debug.Log("Entered Paused State");
    }

    public void ExitState()
    {
        // Clean up paused state
        Debug.Log("Exited Paused State");
    }
}
