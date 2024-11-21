using UnityEngine;

public class Visualizer : MonoBehaviour
{
    public Transform[] visualizerBars;
    public float visualizerSpeed = 10f;
    public float minHeight = 0.1f;
    public float maxHeight = 5f;
    public FFTWindow fftWindow = FFTWindow.Blackman;
    public int sampleCount = 64;

    private float[] spectrumData;

    void Update()
    {
        if (spectrumData == null || spectrumData.Length != sampleCount)
        {
            spectrumData = new float[sampleCount];
        }

        AudioListener.GetSpectrumData(spectrumData, 0, fftWindow);
        float timeFactor = visualizerSpeed * Time.unscaledDeltaTime;

        for (int i = 0; i < visualizerBars.Length && i < sampleCount; i++)
        {
            float intensity = spectrumData[i] * (maxHeight - minHeight) + minHeight;
            Vector3 targetScale = new Vector3(
                visualizerBars[i].localScale.x,
                Mathf.Clamp(intensity, minHeight, maxHeight),
                visualizerBars[i].localScale.z
            );

            visualizerBars[i].localScale = Vector3.Lerp(visualizerBars[i].localScale, targetScale, timeFactor);
        }
    }
}