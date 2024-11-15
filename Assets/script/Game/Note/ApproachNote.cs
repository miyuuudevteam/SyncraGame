using UnityEngine;
using UnityEngine.UI;

public class NoteApproach : MonoBehaviour
{
    public Image outerNote;
    public Image originalNote;
    public float approachSpeed = 2f;
    public float maxTransparency = 0.5f;
    public Vector3 initialOuterNoteScale = new Vector3(2f, 2f, 2f);

    private Vector3 originalScale;
    private bool isApproaching = false;

    void Start()
    {
        originalScale = originalNote.rectTransform.localScale;
        ResetOuterNote();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isApproaching)
        {
            isApproaching = true;
        }

        if (isApproaching)
        {
            ApproachAndChangeTransparency();
        }
    }

    void ApproachAndChangeTransparency()
    {
        outerNote.rectTransform.localScale = Vector3.MoveTowards(
            outerNote.rectTransform.localScale,
            originalScale,
            approachSpeed * Time.deltaTime
        );

        float currentAlpha = outerNote.color.a;
        float newAlpha = Mathf.MoveTowards(currentAlpha, maxTransparency, Time.deltaTime * approachSpeed * 0.5f);
        SetTransparency(newAlpha);

        if (Vector3.Distance(outerNote.rectTransform.localScale, originalScale) < 0.01f)
        {
            SetTransparency(maxTransparency);
            isApproaching = false;
            ResetOuterNote();
        }
    }

    void SetTransparency(float alpha)
    {
        Color color = outerNote.color;
        color.a = Mathf.Clamp(alpha, 0f, 1f);
        outerNote.color = color;
    }

    public void SetCustomTransparency(float alpha)
    {
        SetTransparency(alpha);
    }

    void ResetOuterNote()
    {
        outerNote.rectTransform.localScale = initialOuterNoteScale;
        SetTransparency(0f);
    }
}
