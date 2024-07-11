using UnityEngine;

//Service Locator Pattern
public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
        ServiceLocator.Register<IGameSettingsManager>(new GameSettingsManager());
        ServiceLocator.Register<IGameDataManager>(new GameDataManager());
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<IGameSettingsManager>();
        ServiceLocator.Unregister<IGameDataManager>();
    }
}
