using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visualizer : MonoBehaviour
{
    public float minHeight = 15.0f;
    public float maxHeight = 425.0f;
    public float updateSensitivity = 0.5f;
    public Color visualizerColor = Color.gray;
    public AudioSource audioSource;
    public bool loop = true;
    [Range(64, 8192)]
    public int visualizerSamples = 64;
    public VisualizerObj[] visualizerObjects;

    void Start()
    {
        visualizerObjects = GetComponentsInChildren<VisualizerObj>();

        // Ensure visualizerSamples is a power of two
        visualizerSamples = Mathf.ClosestPowerOfTwo(visualizerSamples);
        visualizerSamples = Mathf.Clamp(visualizerSamples, 64, 8192);

        if (!audioSource)
            return;

        audioSource.loop = loop;
    }

    void Update()
    {
        float[] spectrumData = new float[visualizerSamples];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // Loop through the smaller of the two arrays to avoid IndexOutOfRangeException
        int objectCount = Mathf.Min(visualizerObjects.Length, visualizerSamples);

        for (int i = 0; i < objectCount; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().sizeDelta;

            newSize.y = Mathf.Clamp(Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 5.0f), updateSensitivity), minHeight, maxHeight);
            visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;

            visualizerObjects[i].GetComponent<Image>().color = visualizerColor;
        }
    }
}
