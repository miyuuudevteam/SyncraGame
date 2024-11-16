using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEditor;

public class RendererDropdownManager : MonoBehaviour
{
    public Dropdown rendererDropdown;
    public GameObject restartPromptPanel;

    private List<GraphicsDeviceType> availableRenderers = new List<GraphicsDeviceType>();

    private void Start()
    {
        // Add renderers based on platform compatibility
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

        rendererDropdown.ClearOptions();
        List<string> options = new List<string>();
        int savedRendererIndex = PlayerPrefs.GetInt("RendererIndex", 0);

        // Populate dropdown with filtered renderer options
        for (int i = 0; i < availableRenderers.Count; i++)
        {
            options.Add(availableRenderers[i].ToString());
        }

        rendererDropdown.AddOptions(options);
        rendererDropdown.value = savedRendererIndex;
        rendererDropdown.RefreshShownValue();

        rendererDropdown.onValueChanged.AddListener(OnRendererChanged);

        // Hide restart prompt at the start
        if (restartPromptPanel != null)
        {
            restartPromptPanel.SetActive(false);
        }
    }

    private void OnRendererChanged(int rendererIndex)
    {
        SetRenderer(rendererIndex);

        // Show the restart prompt to notify the player
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

    private void ApplySavedRenderer()
    {
        int savedRendererIndex = PlayerPrefs.GetInt("RendererIndex", 0);
        GraphicsDeviceType selectedRenderer = availableRenderers[savedRendererIndex];

        if (SystemInfo.graphicsDeviceType != selectedRenderer)
        {
            PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows, new GraphicsDeviceType[] { selectedRenderer });
        }
    }

    private void OnDestroy()
    {
        rendererDropdown.onValueChanged.RemoveListener(OnRendererChanged);
    }
}
