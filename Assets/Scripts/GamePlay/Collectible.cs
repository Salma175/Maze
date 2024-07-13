using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public GameObject iconPrefab; // Assign the IconImage prefab in the Inspector
    private Vector3 originalPosition;
    private float fadeDuration = 0.3f;
    private Canvas uiCanvas;
    void Start()
    {
        // Find the Canvas at runtime
        uiCanvas = FindObjectOfType<Canvas>();

        originalPosition = transform.position;

    }
    internal void ResetUi()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(AudioClipName.Coin);
            Collected();
            gameObject.SetActive(false);
        }
    }

    private void Collected()
    {
        // Instantiate the icon
        GameObject icon = Instantiate(iconPrefab, uiCanvas.transform);
        icon.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        Image iconImage = icon.GetComponent<Image>();

        // Create a sequence for the animation
        DG.Tweening.Sequence sequence = DOTween.Sequence();

        // Fade in and scale up
        sequence.Append(iconImage.transform.DOScale(Vector3.one, fadeDuration).From(Vector3.zero));
        sequence.Join(iconImage.DOFade(1f, fadeDuration));

        // Fade out and scale down
        sequence.Append(iconImage.transform.DOScale(Vector3.zero, fadeDuration));
        sequence.Join(iconImage.DOFade(0f, fadeDuration));

        // Optional: Destroy the object after the animation completes
        sequence.OnComplete(() => Destroy(icon));
    }
}
