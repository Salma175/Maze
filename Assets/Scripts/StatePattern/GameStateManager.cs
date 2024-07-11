using UnityEngine;


//Singleton Pattern
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private IGameState currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(new MainMenuState());
    }

    public void ChangeState(IGameState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
