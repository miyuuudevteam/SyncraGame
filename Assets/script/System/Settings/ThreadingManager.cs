using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ThreadingManager : MonoBehaviour
{
    public Dropdown threadingModeDropdown;
    public GameObject restartPromptPanel;

    private List<string> threadingModes = new List<string> { "Single-Threaded", "Multi-Threaded" };

    private void Start()
    {
        threadingModeDropdown.ClearOptions();
        threadingModeDropdown.AddOptions(threadingModes);

        int savedModeIndex = PlayerPrefs.GetInt("ThreadingModeIndex", 0);
        threadingModeDropdown.value = savedModeIndex;
        threadingModeDropdown.RefreshShownValue();

        threadingModeDropdown.onValueChanged.AddListener(OnThreadingModeChanged);

        if (restartPromptPanel != null)
        {
            restartPromptPanel.SetActive(false);
        }

        ApplySavedThreadingMode();
    }

    private void OnThreadingModeChanged(int modeIndex)
    {
        SetThreadingMode(modeIndex);

        if (restartPromptPanel != null)
        {
            restartPromptPanel.SetActive(true);
        }
    }

    private void SetThreadingMode(int modeIndex)
    {
        PlayerPrefs.SetInt("ThreadingModeIndex", modeIndex);
        PlayerPrefs.Save();
    }

    private void ApplySavedThreadingMode()
    {
        int savedModeIndex = PlayerPrefs.GetInt("ThreadingModeIndex", 0);

        if (savedModeIndex == 0)
        {
            Debug.Log("Applied Single-Threaded Mode.");
        }
        else
        {
            Debug.Log("Applied Multi-Threaded Mode.");
        }
    }

    private void OnDestroy()
    {
        threadingModeDropdown.onValueChanged.RemoveListener(OnThreadingModeChanged);
    }
}
