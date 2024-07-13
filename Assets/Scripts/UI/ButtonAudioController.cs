using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(ButtonAnimation))]
public class ButtonAudioController : MonoBehaviour
{
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioClipName.Button);
    }
}
