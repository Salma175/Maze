using DG.Tweening;
using UnityEngine;

public class TweenManager : MonoBehaviour
{

    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private RectTransform rectTransform;

    private float fadeTime = 1f;

    private void OnEnable()
    {
        PanelFadeIn();
    }

    private void PanelFadeIn()
    {
        canvasGroup.alpha = 0f;
        rectTransform.localPosition = new Vector3(0f,-500f, 0f);

        rectTransform.DOAnchorPos(new Vector3(0f,0f,0f),fadeTime,false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1,fadeTime);
    }
}
