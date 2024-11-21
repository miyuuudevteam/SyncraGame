using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Rendering;
public class Renderers : MonoBehaviour
{
    public Dropdown rendererDropdown;
    public GameObject restartPromptPanel;
    private List<GraphicsDeviceType> availableRenderers = new List<GraphicsDeviceType>();
    private ErrorDictionary ErrorCodeManager;
    private ErrorDictionary errorCodeManager;

    private void Start()
    {
        rendererDropdown.ClearOptions();

        PopulateAvailableRenderers();
        List<string> options = new List<string>();
        int savedRendererIndex = PlayerPrefs.GetInt("RendererIndex", 0);

        for (int i = 0; i < availableRenderers.Count; i++)
        {
            options.Add(availableRenderers[i].ToString());
        }

        rendererDropdown.AddOptions(options);

        if (savedRendererIndex >= availableRenderers.Count)
        {
            savedRendererIndex = 0;
        }

        rendererDropdown.value = savedRendererIndex;
        rendererDropdown.RefreshShownValue();
        rendererDropdown.onValueChanged.AddListener(OnRendererChanged);

        if (restartPromptPanel != null)
        {
            restartPromptPanel.SetActive(false);
        }
    }

    private void PopulateAvailableRenderers()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            availableRenderers.Add(GraphicsDeviceType.Direct3D11);
            availableRenderers.Add(GraphicsDeviceType.Direct3D12);
            availableRenderers.Add(GraphicsDeviceType.Vulkan);
            availableRenderers.Add(GraphicsDeviceType.OpenGLCore);
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            availableRenderers.Add(GraphicsDeviceType.Metal);
            availableRenderers.Add(GraphicsDeviceType.OpenGLCore);
        }
        else if (Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.LinuxEditor)
        {
            availableRenderers.Add(GraphicsDeviceType.Vulkan);
            availableRenderers.Add(GraphicsDeviceType.OpenGLCore);
        }
    }

    private void OnRendererChanged(int rendererIndex)
    {
        if (rendererIndex >= availableRenderers.Count)
        {
            ShowUnsupportedRendererError();
            return;
        }

        SetRenderer(rendererIndex);

        if (restartPromptPanel != null)
        {
            restartPromptPanel.SetActive(true);
        }
    }

    private void SetRenderer(int rendererIndex)
    {
        PlayerPrefs.SetInt("RendererIndex", rendererIndex);
        PlayerPrefs.Save();
    }

    private void ShowUnsupportedRendererError()
    {
        if (errorCodeManager != null)
        {
            errorCodeManager.DisplayError("SYC-1001");
        }
        else
        {
            Debug.LogError("ErrorCodeManager not found. Unable to display error.");
        }
    }

    private void OnDestroy()
    {
        rendererDropdown.onValueChanged.RemoveListener(OnRendererChanged);
    }
}