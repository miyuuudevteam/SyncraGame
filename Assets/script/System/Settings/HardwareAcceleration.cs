using UnityEngine;
using UnityEngine.UI;

public class HardwareAcceleration : MonoBehaviour
{
    public Toggle hardwareAccelerationToggle;
    public GameObject restartPromptPanel;

    private void Start()
    {
        bool isHardwareAccelerationEnabled = PlayerPrefs.GetInt("HardwareAcceleration", 0) == 1;
        hardwareAccelerationToggle.isOn = isHardwareAccelerationEnabled;
        hardwareAccelerationToggle.onValueChanged.AddListener(OnHardwareAccelerationChanged);

        if (restartPromptPanel != null)
        {
            restartPromptPanel.SetActive(false);
        }
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
    }

    private void OnDestroy()
    {
        hardwareAccelerationToggle.onValueChanged.RemoveListener(OnHardwareAccelerationChanged);
    }
}
