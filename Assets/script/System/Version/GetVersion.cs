using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetVersion : MonoBehaviour
{
    [SerializeField] private string url = "https://syncra-api.vercel.app/data.json";
    [SerializeField] private Text textComponent;

    void Start()
    {
        textComponent.text = "Attempting to connect to development api server...";
        StartCoroutine(FetchJsonData());
    }

    private IEnumerator FetchJsonData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                yield return new WaitForSeconds(5f);
                ProcessJson(jsonResponse);
            }
            else
            {
                Debug.LogError("Error fetching JSON: " + webRequest.error);
                textComponent.text = "Unable to get JSON content from the server.";
            }
        }
    }

    private void ProcessJson(string jsonResponse)
    {
        try
        {
            JsonData data = JsonUtility.FromJson<JsonData>(jsonResponse);
            textComponent.text = data.text;
        }
        catch
        {
            textComponent.text = "Error occurred";
        }
    }

    [System.Serializable]
    private class JsonData
    {
        public string text;
    }
}
