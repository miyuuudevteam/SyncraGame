using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Highlight Settings")]
    public Image targetImage; // The Image component to highlight
    public Color imageHighlightColor = Color.white;
    public Color imagePressColor = Color.gray;

    public Text targetText; // The legacy Text component to highlight
    public Color textHighlightColor = Color.white;
    public Color textPressColor = Color.gray;

    [Header("Default Colors")]
    public Color defaultImageColor = Color.white;
    public Color defaultTextColor = Color.white;

    [Header("Flicker Settings")]
    public float flickerSpeed = 0.1f;
    public float highlightTransitionSpeed = 0.2f;
    public Color flickerColor = Color.gray; // The color to flicker to

    private bool isHovering = false;
    private Coroutine flickerCoroutine;

    private void Start()
    {
        // Set the default colors at the start
        if (targetImage != null)
            targetImage.color = defaultImageColor;

        if (targetText != null)
            targetText.color = defaultTextColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (flickerCoroutine != null)
            StopCoroutine(flickerCoroutine);
        flickerCoroutine = StartCoroutine(SmoothFlickerEffect());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (flickerCoroutine != null)
            StopCoroutine(flickerCoroutine);
        StartCoroutine(TransitionToDefault());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Change to press colors temporarily
        if (targetImage != null)
            targetImage.color = imagePressColor;

        if (targetText != null)
            targetText.color = textPressColor;

        StartCoroutine(TransitionToHighlight());
    }

    private IEnumerator SmoothFlickerEffect()
    {
        float t = 0;
        while (isHovering)
        {
            while (t < 1 && isHovering)
            {
                t += Time.deltaTime / flickerSpeed;
                Color currentImageColor = Color.Lerp(imageHighlightColor, flickerColor, Mathf.PingPong(t, 1));
                Color currentTextColor = Color.Lerp(textHighlightColor, flickerColor, Mathf.PingPong(t, 1));

                if (targetImage != null)
                    targetImage.color = currentImageColor;

                if (targetText != null)
                    targetText.color = currentTextColor;

                yield return null;
            }
            t = 0;
        }
    }

    private IEnumerator TransitionToDefault()
    {
        float t = 0;
        Color initialImageColor = targetImage != null ? targetImage.color : defaultImageColor;
        Color initialTextColor = targetText != null ? targetText.color : defaultTextColor;

        while (t < 1)
        {
            t += Time.deltaTime / highlightTransitionSpeed;

            if (targetImage != null)
                targetImage.color = Color.Lerp(initialImageColor, defaultImageColor, t);

            if (targetText != null)
                targetText.color = Color.Lerp(initialTextColor, defaultTextColor, t);

            yield return null;
        }
    }

    private IEnumerator TransitionToHighlight()
    {
        float t = 0;
        Color initialImageColor = targetImage != null ? targetImage.color : imageHighlightColor;
        Color initialTextColor = targetText != null ? targetText.color : textHighlightColor;

        while (t < 1)
        {
            t += Time.deltaTime / highlightTransitionSpeed;

            if (targetImage != null)
                targetImage.color = Color.Lerp(initialImageColor, imageHighlightColor, t);

            if (targetText != null)
                targetText.color = Color.Lerp(initialTextColor, textHighlightColor, t);

            yield return null;
        }
    }
}
