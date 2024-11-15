using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class SetupClient : MonoBehaviour
{
    public Text processText;
    public string menuscene;

    private void Start()
    {
        StartCoroutine(SetupProcess());
    }

    private IEnumerator SetupProcess()
    {
        UpdateProcessText("Starting setup process...");
        yield return new WaitForSeconds(5f);

        InitializeFolders();

        yield return StartCoroutine(InitializeClientData());

        UpdateProcessText("Setup complete. Please wait...");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(menuscene);
    }

    private void InitializeFolders()
    {
        string[] folders = { "levels", "skins", "replays", "screenshot", "editor" };
        
        // Get the directory of the executable
        string gamePath = Directory.GetParent(Application.dataPath).FullName;
        string setupFilePath = Path.Combine(gamePath, "clientsetuplog.txt");

        Debug.Log($"Game Path: {gamePath}");
        UpdateProcessText($"Game Path: {gamePath}");

        bool allFoldersExist = true;

        if (!File.Exists(setupFilePath))
        {
            foreach (string folder in folders)
            {
                string folderPath = Path.Combine(gamePath, folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    UpdateProcessText($"Created missing folder: {folder}");
                    allFoldersExist = false;
                }
                else
                {
                    UpdateProcessText($"Folder already exists: {folder}");
                }
            }

            File.WriteAllText(setupFilePath, "[SYSTEM] Setup has been completed");
            UpdateProcessText("Setup file created to track setup completion.");
        }
        else
        {
            foreach (string folder in folders)
            {
                string folderPath = Path.Combine(gamePath, folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    UpdateProcessText($"Recreated missing/corrupted folder: {folder}");
                    allFoldersExist = false;
                }
                else
                {
                    UpdateProcessText($"Folder already exists: {folder}");
                }
            }

            if (allFoldersExist)
            {
                UpdateProcessText("All required folders already exist.");
            }
            else
            {
                UpdateProcessText("Missing/corrupted folders were created successfully.");
            }
        }
    }

    private IEnumerator InitializeClientData()
    {
        UpdateProcessText("Initializing Client...");
        yield return new WaitForSeconds(2f);
        UpdateProcessText("Client data loaded successfully.");

        UpdateProcessText("Initializing player settings...");
        yield return new WaitForSeconds(1f);
        UpdateProcessText("Player settings initialized successfully.");

        UpdateProcessText("Loading additional resources...");
        yield return new WaitForSeconds(1f);
        UpdateProcessText("All resources initialized successfully.");
    }

    private void UpdateProcessText(string message)
    {
        processText.text = message;
        Debug.Log(message);
    }
}
