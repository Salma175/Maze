using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
        // Registering game settings manager with the service locator
        ServiceLocator.Register<IGameSettingsManager>(new GameSettingsManager());
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<IGameSettingsManager>();
    }
}
