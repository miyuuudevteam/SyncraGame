using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BounceToBeat : MonoBehaviour
{
    public AudioSource audioSource;
    public float sensitivity = 50f;
    public float smoothness = 5f;
    public float minScale = 1f;
    public float maxScale = 2f;

    private Image image;
    private Vector3 initialScale;
    private float[] spectrumData = new float[64];

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned!");
            return;
        }

        image = GetComponent<Image>();
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
            
            float intensity = 0f;
            for (int i = 0; i < spectrumData.Length; i++)
            {
                intensity += spectrumData[i];
            }
            intensity /= spectrumData.Length;
            
            float targetScale = Mathf.Lerp(minScale, maxScale, intensity * sensitivity);
            
            Vector3 newScale = Vector3.Lerp(transform.localScale, initialScale * targetScale, Time.deltaTime * smoothness);
            transform.localScale = newScale;
        }
    }
}