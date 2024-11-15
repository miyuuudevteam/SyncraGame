using UnityEngine;
using UnityEngine.UI;

public class VSyncManager : MonoBehaviour
{
    public Toggle vsyncToggle;

    private void Start()
    {
        int vsyncSetting = PlayerPrefs.GetInt("VSyncEnabled", 0);
        vsyncToggle.isOn = vsyncSetting == 1;
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;
        vsyncToggle.onValueChanged.AddListener(OnVSyncToggleChanged);
    }

    private void OnVSyncToggleChanged(bool isOn)
    {
        QualitySettings.vSyncCount = isOn ? 1 : 0;
        PlayerPrefs.SetInt("VSyncEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        vsyncToggle.onValueChanged.RemoveListener(OnVSyncToggleChanged);
    }
}
