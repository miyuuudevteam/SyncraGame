using UnityEngine;
using UnityEngine.UI;
using Unity.Jobs;
using Unity.Collections;

public class HardwareAcceleration : MonoBehaviour
{
    public Toggle hardwareAccelerationToggle;
    public GameObject restartPromptPanel;
    
    private bool isHardwareAccelerationEnabled;

    private void Start()
    {
        isHardwareAccelerationEnabled = PlayerPrefs.GetInt("HardwareAcceleration", 0) == 1;
        hardwareAccelerationToggle.isOn = isHardwareAccelerationEnabled;
        hardwareAccelerationToggle.onValueChanged.AddListener(OnHardwareAccelerationChanged);

        if (restartPromptPanel != null)
        {
            restartPromptPanel.SetActive(false);
        }

        ApplyHardwareAccelerationSetting();
    }

    private void OnHardwareAccelerationChanged(bool isEnabled)
    {
        SetHardwareAcceleration(isEnabled);

        if (restartPromptPanel != null)
        {
            restartPromptPanel.SetActive(true);
        }
    }

    private void SetHardwareAcceleration(bool isEnabled)
    {
        PlayerPrefs.SetInt("HardwareAcceleration", isEnabled ? 1 : 0);
        PlayerPrefs.Save();
        
        isHardwareAccelerationEnabled = isEnabled;

        ApplyHardwareAccelerationSetting();
    }

    private void ApplyHardwareAccelerationSetting()
    {
        if (isHardwareAccelerationEnabled)
        {
            Debug.Log("Hardware Acceleration Enabled - Using GPU for calculations.");
            UseGPUForTasks();
        }
        else
        {
            Debug.Log("Hardware Acceleration Disabled - Using CPU for calculations.");
            UseCPUForTasks();
        }
    }

    private void UseGPUForTasks()
    {
        ComputeShader computeShader = Resources.Load<ComputeShader>("MyComputeShader");
        int kernelHandle = computeShader.FindKernel("CSMain");

        computeShader.Dispatch(kernelHandle, 1, 1, 1);
    }

    private void UseCPUForTasks()
    {
        MyJob job = new MyJob
        {
            value = 100
        };

        JobHandle jobHandle = job.Schedule();
        jobHandle.Complete();
    }

    private void OnDestroy()
    {
        hardwareAccelerationToggle.onValueChanged.RemoveListener(OnHardwareAccelerationChanged);
    }

    private struct MyJob : IJob
    {
        public int value;

        public void Execute()
        {
            for (int i = 0; i < value; i++)
            {
                Debug.Log($"CPU Job Running: {i}");
            }
        }
    }
}