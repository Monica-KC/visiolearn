using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RAGQuery : MonoBehaviour
{
    // UI Elements (set these in the Unity Inspector)
    public InputField questionInputField; // Drag your InputField UI element here
    public Text responseText;            // Drag your Text UI element here

    // API URL dynamically loaded from the file
    private string ApiUrl;

    // Path to the file containing the ngrok URL (located in the Unity Assets folder)
    // private string NgrokUrlFilePath => Path.Combine(Application.dataPath, "ngrok_url.txt");
    // Start is called before the first frame update
    void Start()
    {
        // Load the ngrok URL from the file
        try
        {
            // if (File.Exists(NgrokUrlFilePath))
            // {
                ApiUrl = "https://b2d5-2401-4900-6318-536c-ec14-d709-5e6f-7c6.ngrok-free.app";
                Debug.Log($"API URL loaded: {ApiUrl}");
            // }
            // else
            // {
            //     Debug.LogError($"ngrok URL file not found at: {NgrokUrlFilePath}");
            //     responseText.text = "Error: ngrok URL file not found. Ensure the server is running and ngrok is configured.";
            // }
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to read ngrok URL file: {e.Message}");
            responseText.text = "Error: Could not load API URL. Check the console for details.";
        }
    }

    // Triggered when the Submit button is clicked
    public void OnSubmitQuery()
    {
        string question = questionInputField.text; // Get the user input from the InputField
        if (string.IsNullOrEmpty(ApiUrl))
        {
            responseText.text = "Error: API URL is not set. Ensure the server is running.";
            return;
        }

        if (string.IsNullOrEmpty(question))
        {
            responseText.text = "Please enter a question.";
            return;
        }

        StartCoroutine(QueryRAG(question));
    }

    // Define the structure of the JSON payload
    [System.Serializable]
    public class QueryPayload
    {
        public string question;
    }

    // Define the structure of the JSON response
    [System.Serializable]
    public class QueryResponse
    {
        public string answer;
    }

    // Coroutine to send a query to the FastAPI server
    private IEnumerator QueryRAG(string question)
    {
        // Create the JSON payload
        QueryPayload payload = new QueryPayload { question = question };
        string jsonPayload = JsonUtility.ToJson(payload);

        // Prepare the UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(ApiUrl + "/query", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Log the outgoing request for debugging
        Debug.Log($"Sending query to: {ApiUrl}/query");
        Debug.Log($"Payload: {jsonPayload}");

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Parse and display the response
            string jsonResponse = request.downloadHandler.text;
            Debug.Log($"Response received: {jsonResponse}");

            QueryResponse response = JsonUtility.FromJson<QueryResponse>(jsonResponse);
            responseText.text = response.answer;
        }
        else
        {
            // Handle request errors
            Debug.LogError($"Request failed: {request.error}");
            responseText.text = $"Error: {request.error}. Check server logs for more details.";
        }
    }
}
