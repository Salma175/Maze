using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Button button;
    private RectTransform rectTransform;

    // Animation settings
    private float hoverScale = 1.1f;
    private float clickScale = 0.9f;
    private float duration = 0.2f;

    private Vector3 originalScale;

    private void Awake()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOScale(hoverScale, duration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOScale(originalScale, duration).SetEase(Ease.OutBack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        rectTransform.DOScale(clickScale, duration).SetEase(Ease.OutBack)
            .OnComplete(() => rectTransform.DOScale(originalScale, duration).SetEase(Ease.OutBack));
    }
}
