using UnityEngine;

public class UIMnager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainscreen;
    [SerializeField]
    private GameObject _levelFailScreen;
    [SerializeField]
    private GameObject _levelPassScreen;
    [SerializeField]
    private GameObject _settingsScreen;
    [SerializeField]
    private GameObject _infoScreen;
    [SerializeField]
    private GameObject _gameScreen;

    void Start()
    {
        ResetUi();
    }

    public void ShowInfo()
    {
        AudioManager.Instance.PlaySFX(AudioClipName.Button);
        _infoScreen.SetActive(true);
    }

    public void HideInfo() 
    {
        AudioManager.Instance.PlaySFX(AudioClipName.Button);
        _infoScreen.SetActive(false);
    }

    private void ResetUi()
    {
        _infoScreen.SetActive(false);
    }
}
