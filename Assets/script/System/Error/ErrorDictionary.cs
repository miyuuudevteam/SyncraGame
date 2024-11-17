using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ErrorCodeManager : MonoBehaviour
{
    // Dictionary of error codes and their messages
    private Dictionary<string, string> errorMessages = new Dictionary<string, string>
    {
        // Graphics Errors (Starting with SYC-1xxx)
        { "SYC-1001", "Unsupported Graphics Renderer\n• The selected graphics renderer is not supported by the player’s system.\n• Message: \"Unsupported Graphics Renderer. Error Code: SYC-1001\"" },
        { "SYC-1002", "Graphics Driver Outdated\n• The graphics driver is outdated or incompatible with the selected renderer.\n• Message: \"Graphics Driver Outdated. Error Code: SYC-1002\"" },
        { "SYC-1003", "Graphics Renderer Initialization Failed\n• The selected renderer failed to initialize.\n• Message: \"Graphics Renderer Initialization Failed. Error Code: SYC-1003\"" },
        
        // Network Errors (Starting with SYC-2xxx)
        { "SYC-2001", "Network Connection Failed\n• The game was unable to connect to the server.\n• Message: \"Network Connection Failed. Error Code: SYC-2001\"" },
        { "SYC-2002", "Server Timeout\n• The connection to the server timed out.\n• Message: \"Server Timeout. Error Code: SYC-2002\"" },
        { "SYC-2003", "Invalid Network Settings\n• The network settings are invalid or misconfigured.\n• Message: \"Invalid Network Settings. Error Code: SYC-2003\"" },
        { "SYC-2004", "Lost Connection\n• The connection to the server was lost.\n• Message: \"Lost Connection. Error Code: SYC-2004\"" },
        
        // Miscellaneous Errors (Starting with SYC-3xxx)
        { "SYC-3001", "Unexpected Error\n• An unexpected error occurred while running the game.\n• Message: \"Unexpected Error. Error Code: SYC-3001\"" },
        { "SYC-3002", "File Not Found\n• A required file was not found on the system.\n• Message: \"File Not Found. Error Code: SYC-3002\"" },
        { "SYC-3003", "Insufficient Permissions\n• The game does not have the necessary permissions to perform this action.\n• Message: \"Insufficient Permissions. Error Code: SYC-3003\"" },
        { "SYC-3004", "Corrupted Installation\n• The game installation is corrupted and cannot proceed.\n• Message: \"Corrupted Installation. Error Code: SYC-3004\"" },
        { "SYC-3005", "Out of Memory\n• The system ran out of memory during the game process.\n• Message: \"Out of Memory. Error Code: SYC-3005\"" },
        
        // Custom Errors (Starting with SYC-4xxx)
        { "SYC-4001", "Database Connection Failed\n• The game was unable to connect to the required database.\n• Message: \"Database Connection Failed. Error Code: SYC-4001\"" },
        { "SYC-4002", "User Authentication Failed\n• The user’s authentication failed.\n• Message: \"User Authentication Failed. Error Code: SYC-4002\"" },
    };

    public void ShowError(string errorCode)
    {
        if (errorMessages.ContainsKey(errorCode))
        {
            string errorMessage = errorMessages[errorCode];
            ShowSystemErrorPrompt(errorCode, errorMessage);
        }
        else
        {
            Debug.LogError("Error code not found!");
        }
    }

    private void ShowSystemErrorPrompt(string errorCode, string errorMessage)
    {
        string message = $"{errorMessage}\n\nError Code: {errorCode}";

        #if UNITY_STANDALONE_WIN
            MessageBox(IntPtr.Zero, message, "Game Error", 0);
        #elif UNITY_STANDALONE_OSX
            Process.Start("osascript", $"-e 'display dialog \"{message}\" with title \"Game Error\"'");
        #elif UNITY_STANDALONE_LINUX
            Process.Start("zenity", $"--error --text=\"{message}\"");
        #else
            Debug.LogError("Unsupported platform.");
        #endif
    }

    // Windows system prompt
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool MessageBox(IntPtr hWnd, string text, string caption, uint type);
}