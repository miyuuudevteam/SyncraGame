using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class UnityAudioSourceRework : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip Clip { get => audioSource.clip; set => audioSource.clip = value; }

    [SerializeField]
    private float fadeTime = 1f;

    [SerializeField]
    private AnimationCurve volumeCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private float targetVolume;
    private float volumeVelocity;

    [SerializeField]
    private List<float> beatTimings = new List<float>();

    public delegate void BeatAction(int beatIndex);
    public event BeatAction OnBeat;

    private int currentBeatIndex = 0;
    private const float beatThreshold = 0.05f; // 50ms threshold

    [SerializeField, Range(0f, 3f)]
    private float pitch = 1f;
    [SerializeField, Range(0.1f, 3f)]
    private float speed = 1f;
    [SerializeField, Range(0f, 3f)]
    private float bassBoost = 1f;

    private AudioLowPassFilter lowPassFilter;
    private AudioHighPassFilter highPassFilter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        targetVolume = audioSource.volume;

        lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
        highPassFilter = gameObject.AddComponent<AudioHighPassFilter>();

        UpdateAudioSettings();
    }

    private void Update()
    {
        UpdateVolume();
        CheckForBeats();
    }

    private void UpdateVolume()
    {
        if (audioSource.volume != targetVolume)
        {
            audioSource.volume = Mathf.SmoothDamp(audioSource.volume, targetVolume, ref volumeVelocity, fadeTime);
        }
    }

    private void CheckForBeats()
    {
        if (audioSource.isPlaying && currentBeatIndex < beatTimings.Count)
        {
            if (Mathf.Abs(audioSource.time - beatTimings[currentBeatIndex]) < beatThreshold)
            {
                OnBeat?.Invoke(currentBeatIndex);
                currentBeatIndex++;
            }
        }
    }

    private void UpdateAudioSettings()
    {
        audioSource.pitch = pitch * speed;

        lowPassFilter.cutoffFrequency = Mathf.Lerp(10, 22000, 1 - bassBoost);
        highPassFilter.cutoffFrequency = Mathf.Lerp(10, 22000, bassBoost);
    }

    public void Play()
    {
        audioSource.Play();
        currentBeatIndex = 0;
    }

    public void Stop()
    {
        audioSource.Stop();
        currentBeatIndex = 0;
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }

    public void SetVolume(float volume, bool immediate = false)
    {
        targetVolume = volumeCurve.Evaluate(volume);
        if (immediate)
        {
            audioSource.volume = targetVolume;
        }
    }

    public void FadeTo(float targetVolume, float duration)
    {
        this.targetVolume = volumeCurve.Evaluate(targetVolume);
        fadeTime = duration;
    }

    public void SetPitch(float newPitch)
    {
        pitch = Mathf.Clamp(newPitch, 0f, 3f);
        UpdateAudioSettings();
    }

    public void SetSpeed(float newSpeed)
    {
        speed = Mathf.Clamp(newSpeed, 0.1f, 3f);
        UpdateAudioSettings();
    }

    public void SetBassBoost(float boost)
    {
        bassBoost = Mathf.Clamp01(boost);
        UpdateAudioSettings();
    }

    public void SetBeatTimings(List<float> newBeatTimings)
    {
        beatTimings = newBeatTimings;
        currentBeatIndex = 0;
    }

    public float GetAudioTime()
    {
        return audioSource.time;
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public float GetPitch()
    {
        return pitch;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetBassBoost()
    {
        return bassBoost;
    }
}