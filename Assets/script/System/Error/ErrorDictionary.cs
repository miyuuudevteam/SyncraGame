using UnityEngine;
using System.Collections.Generic;

public class ErrorDictionary : MonoBehaviour
{
    private Dictionary<string, string> errorCodes = new Dictionary<string, string>();

    private void Start()
    {
        InitializeErrorCodes();
    }

    private void InitializeErrorCodes()
    {
        errorCodes.Add("SYC-1001", 
            "Unsupported Graphics Renderer\n• The selected graphics renderer is not supported by the player’s system.\n• Message: \"Unsupported Graphics Renderer. Error Code: SYC-1001\"");
        errorCodes.Add("SYC-1002", 
            "Renderer Initialization Failure\n• The game failed to initialize the selected renderer due to compatibility issues or system limitations.\n• Message: \"Renderer Initialization Failed. Error Code: SYC-1002\"");
        errorCodes.Add("SYC-1003", 
            "Renderer Switching Error\n• Switching from one renderer to another failed due to incompatible settings or hardware.\n• Message: \"Error switching to the selected renderer. Error Code: SYC-1003\"");
        errorCodes.Add("SYC-1004", 
            "Unsupported Graphics API\n• The selected graphics API is not supported by the player’s system.\n• Message: \"Unsupported Graphics API. Error Code: SYC-1004\"");
        errorCodes.Add("SYC-1005", 
            "Low Hardware Specifications\n• The system's hardware is insufficient to run the game with the selected renderer.\n• Message: \"Your hardware does not meet the minimum requirements for the selected renderer. Error Code: SYC-1005\"");
        errorCodes.Add("SYC-1006", 
            "Graphics Driver Issue\n• There is an issue with the player's graphics drivers, preventing the renderer from functioning correctly.\n• Message: \"Graphics driver issue detected. Please update your drivers. Error Code: SYC-1006\"");
        errorCodes.Add("SYC-1007", 
            "Renderer Compatibility Issue\n• There is a conflict between the renderer and other game settings or hardware.\n• Message: \"Renderer compatibility issue detected. Error Code: SYC-1007\"");
        errorCodes.Add("SYC-1008", 
            "Shader Compilation Failure\n• The shaders failed to compile, potentially due to unsupported hardware or API issues.\n• Message: \"Shader compilation failure. Error Code: SYC-1008\"");
        errorCodes.Add("SYC-1009", 
            "Outdated Graphics Hardware\n• The graphics hardware is too old to support the modern features required by the renderer.\n• Message: \"Outdated graphics hardware detected. Error Code: SYC-1009\"");
        errorCodes.Add("SYC-1010", 
            "Unsupported GPU Feature\n• The selected renderer requires GPU features that are not supported by the player's system.\n• Message: \"Unsupported GPU feature. Error Code: SYC-1010\"");

        errorCodes.Add("SYC-3001", 
            "File Not Found\n• A critical file required by the game could not be found.\n• Message: \"Required file not found. Error Code: SYC-3001\"");
        errorCodes.Add("SYC-3002", 
            "Insufficient Disk Space\n• There is not enough disk space to run or update the game.\n• Message: \"Insufficient disk space. Error Code: SYC-3002\"");
        errorCodes.Add("SYC-3003", 
            "Invalid Game Assets\n• A problem with game assets has been detected, possibly corrupt or incompatible files.\n• Message: \"Invalid game assets. Error Code: SYC-3003\"");
        errorCodes.Add("SYC-3004", 
            "General Internal Error\n• An unknown error occurred within the game.\n• Message: \"An unexpected internal error occurred. Error Code: SYC-3004\"");
        errorCodes.Add("SYC-3005", 
            "Outdated Game Version\n• The game version is outdated and needs to be updated to the latest version.\n• Message: \"Game version is outdated. Please update the game. Error Code: SYC-3005\"");
    }

    public string GetErrorMessage(string errorCode)
    {
        if (errorCodes.ContainsKey(errorCode))
        {
            return errorCodes[errorCode];
        }
        else
        {
            return "Error: Unknown error code.";
        }
    }

    public void DisplayError(string errorCode)
    {
        string errorMessage = GetErrorMessage(errorCode);
        Debug.LogError(errorMessage);
    }
}