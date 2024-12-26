using UnityEngine;

public class Visualizer : MonoBehaviour
{
    public Transform[] visualizerBars;
    public float visualizerSpeed = 10f;
    public float minHeight = 0.1f;
    public float maxHeight = 5f;
    public FFTWindow fftWindow = FFTWindow.Blackman;
    public int sampleCount = 64;
    public Gradient colorGradient;

    public AudioSource audioSource;
    private float[] spectrumData;

    void Update()
    {
        sampleCount = Mathf.ClosestPowerOfTwo(sampleCount);
        sampleCount = Mathf.Clamp(sampleCount, 64, 8192);

        if (spectrumData == null || spectrumData.Length != sampleCount)
        {
            spectrumData = new float[sampleCount];
        }

        audioSource.GetSpectrumData(spectrumData, 0, fftWindow);

        float timeFactor = visualizerSpeed * Time.unscaledDeltaTime;

        for (int i = 0; i < visualizerBars.Length && i < sampleCount; i++)
        {
            float intensity = spectrumData[i] * (maxHeight - minHeight) + minHeight;
            intensity = Mathf.Clamp(intensity, minHeight, maxHeight);

            Vector3 targetScale = new Vector3(
                visualizerBars[i].localScale.x,
                intensity,
                visualizerBars[i].localScale.z
            );

            visualizerBars[i].localScale = Vector3.Lerp(visualizerBars[i].localScale, targetScale, timeFactor);

            Renderer renderer = visualizerBars[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                float normalizedIntensity = (intensity - minHeight) / (maxHeight - minHeight);
                renderer.material.color = colorGradient.Evaluate(normalizedIntensity);
            }
        }
    }
}