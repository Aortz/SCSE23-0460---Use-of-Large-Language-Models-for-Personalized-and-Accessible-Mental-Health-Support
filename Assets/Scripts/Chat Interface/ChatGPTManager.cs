using UnityEngine.Networking;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OpenAI;
using AssistantModel;
using ThreadModels;
using MessageModel;
using RunModel;
using TMPro;
using HelperFunction;

public class ChatGPTManager : MonoBehaviour
{
    // public OnResponseEvent OnResponse;
    public GameObject chatBubblePrefab; // Assign in the Inspector
    public Transform chatContentPanel; // Assign in the Inspector
    private GameObject currentChatBubble;
    public LoaderController loaderController;
    public GameObject personalityPanel; // Assign in the Inspector

    private int assistantIndex = 0;
    

    [SerializeField] private TextMeshProUGUI currentIndex;
    [SerializeField] private TextMeshProUGUI maxIndex;

    private readonly string fetchAssistantAPIURL = "https://api.openai.com/v1/assistants?order=desc&limit=20";
    private readonly string threadsAPIURL = "https://api.openai.com/v1/threads";
    private readonly string apiKey = "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"; // Replace with your API Key
    private int currentMessageIndex = 0;
    private string[] messages = new string[] { "Hello", "How are you?", "I'm fine, thank you." };
    private OpenAIApi OpenAI = new OpenAIApi("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"); // Replace with your API Key
    // private List<ChatMessage> messages = new List<ChatMessage>();
    private string threadID = "";
    private string assistantID = "";
    public Sprite tickSprite; // Reference to your tick icon sprite
    public GameObject uploadButton; // Reference to your upload button

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetOpenAIAssistants(assistantIndex));
        StartCoroutine(CreateThreads());
    }

    public void askChatGPT(string newText)
    {
        string messageURL = "https://api.openai.com/v1/threads/" + threadID + "/messages";
        // Create a new message payload object        
        MessagePayload newMessage = new MessagePayload();
        // Role is set to "user" to indicate that the message is from the user	
        newMessage.role = "user";
        
        // Add text content
        // TextContent textContent = new TextContent
        // {
        //     Value = newText
        // };
        // Debug.Log("Text Content: " + textContent.Value);
        // Debug.Log("Text Content Type: " + textContent.Type);
        // Debug.Log("Text Content Value: " + textContent.Value);
        // newMessage.Content.Add(textContent);
        // Debug.Log("New Message Content: " + newMessage.Content[0]);

        newMessage.content = newText;
        

        // Convert the payload object to JSON string
        string messageData = JsonUtility.ToJson(newMessage);

        // Start the coroutine to send the message
        Debug.Log("Message Data: " + messageData);
        StartCoroutine(SendMessage(messageURL, messageData));

    }

    public void OpenFileBrowser()
    {
        // Open the file picker
#if UNITY_EDITOR || UNITY_STANDALONE
        string path = UnityEditor.EditorUtility.OpenFilePanel("Select an image", "", "jpg,png");
        if (!string.IsNullOrEmpty(path))
        {
            StartCoroutine(LoadImage(path));
        }
#endif
    }

    IEnumerator LoadImage(string path)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("file://" + path);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Get the loaded texture
            uploadButton.GetComponent<UnityEngine.UI.Image>().sprite = tickSprite;
            // Apply the texture to your UI element or object as needed
            Debug.Log("Image successfully loaded");
        }
    }
    public void togglePersonalityPanel()
    {
        if (personalityPanel.activeSelf)
        {
            personalityPanel.SetActive(false);
        }
        else
        {
            personalityPanel.SetActive(true);
        }
    }

    public void UpdateTherapist()
    {
        assistantIndex = 0;
        togglePersonalityPanel();
        // Restart the coroutine with the updated value
        StopCoroutine(GetOpenAIAssistants(assistantIndex));
        StopCoroutine(CreateThreads());
        StartCoroutine(GetOpenAIAssistants(assistantIndex));
        StartCoroutine(CreateThreads());
    }

    // Method to update the value and text display
    public void UpdateSarcasm()
    {
        assistantIndex = 1;
        togglePersonalityPanel();
        // Restart the coroutine with the updated value
        StopCoroutine(GetOpenAIAssistants(assistantIndex));
        StopCoroutine(CreateThreads());
        StartCoroutine(GetOpenAIAssistants(assistantIndex));
        StartCoroutine(CreateThreads());
    }

    public void UpdateFunny()
    {
        assistantIndex = 2;
        togglePersonalityPanel();
        // Restart the coroutine with the updated value
        StopCoroutine(GetOpenAIAssistants(assistantIndex));
        StopCoroutine(CreateThreads());
        StartCoroutine(GetOpenAIAssistants(assistantIndex));
        StartCoroutine(CreateThreads());
    }

    IEnumerator GetOpenAIAssistants(int assistantIndex)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(fetchAssistantAPIURL))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);
            webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v1");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Use the setter of the AssistantResponseData property to store the response
                string _assistantResponseData = webRequest.downloadHandler.text;
                AssistantResponse AssistantResponseData = JsonUtility.FromJson<AssistantResponse>(_assistantResponseData);
                assistantID = AssistantResponseData.data[assistantIndex].id;
                Debug.Log("Received: " + AssistantResponseData.data[assistantIndex].id);

            }
        }
    }


    IEnumerator CreateThreads()
    {
        // If you have actual data to send, construct a JSON string here
        string jsonData = "{}"; // Currently sending an empty JSON object

        // using (UnityWebRequest webRequest = UnityWebRequest.Post(threadsAPIURL))
        using (UnityWebRequest webRequest = new UnityWebRequest(threadsAPIURL, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);
            webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v1");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Use the setter of the ThreadResponseData property to store the response
                string _threadResponseData = webRequest.downloadHandler.text;
                ThreadResponse ThreadResponseData = JsonUtility.FromJson<ThreadResponse>(_threadResponseData);
                threadID = ThreadResponseData.id;
                // Debug.Log("Thread ID: " + ThreadResponseData.id);
            }
        }
    }

    IEnumerator SendMessage(string messageURL, string messageData)
    {
        // If you have actual data to send, construct a JSON string here
        // string jsonData = "{}"; // Currently sending an empty JSON object
        Debug.Log("Message Data: " + messageData);
        using (UnityWebRequest webRequest = new UnityWebRequest(messageURL, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(messageData);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);
            webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v1");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Use the setter of the ThreadResponseData property to store the response
                string _messageResponseData = webRequest.downloadHandler.text;
                MessageResponse MessageResponseData = JsonUtility.FromJson<MessageResponse>(_messageResponseData);
                Debug.Log("Message ID: " + MessageResponseData.id);
                StartCoroutine(StartConversationRun());
            }
        }
    }

    IEnumerator StartConversationRun()
    {
        string runURL = "https://api.openai.com/v1/threads/" + threadID + "/runs";
        RunPayload newRun = new RunPayload { assistant_id = assistantID };
        string runMessageData = JsonUtility.ToJson(newRun);
        Debug.Log("Run Data: " + runMessageData);

        RunResponse RunResponseData = new RunResponse();

        Debug.Log("Sending web request now");
        using (UnityWebRequest webRequest = new UnityWebRequest(runURL, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(runMessageData);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);
            webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v1");

            yield return webRequest.SendWebRequest(); // This line waits for the web request to complete
            // The code below this line will not execute until the web request is complete
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string _runResponseData = webRequest.downloadHandler.text;
                RunResponseData = JsonUtility.FromJson<RunResponse>(_runResponseData);
                // The status property setter will check the status and potentially trigger the OnCompleted event
                if (RunResponseData.status == "completed")
                {
                    StartCoroutine(GetChatbotResponse());
                }
                else
                {
                    StartCoroutine(CheckRunStatus(threadID, RunResponseData.id));
                }
            }
        }
    }

    // Coroutine for Polling the Run Status
    IEnumerator CheckRunStatus(string threadId, string runId)
    {
        string runStatusURL = $"https://api.openai.com/v1/threads/{threadId}/runs/{runId}";
        RunResponse runResponse = new RunResponse();
        loaderController.ShowLoader(); // Show the loader at the start

        while (true) // Loop indefinitely until we break out when status is "completed"
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(runStatusURL))
            {
                webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);
                webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v1");

                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                    webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error fetching run status: " + webRequest.error);
                    yield break; // Exit the coroutine on error
                }
                else
                {
                    runResponse = JsonUtility.FromJson<RunResponse>(webRequest.downloadHandler.text);
                    if (runResponse.status == "completed")
                    {
                        // Run has completed, trigger any completion logic here
                        // Debug.Log("Run has completed.");
                        loaderController.HideLoader(); // Hide the loader when done
                        StartCoroutine(GetChatbotResponse());
                        yield break; // Exit the coroutine
                    }
                    else
                    {
                        // Debug.Log("Current run status: " + runResponse.status);
                        // Wait for a specified time before polling again
                        yield return new WaitForSeconds(2); // Adjust the delay as needed
                    }
                }
            }
        }
    }


    IEnumerator GetChatbotResponse()
    {
        Debug.Log("Getting chatbot response");
        string messageURL = "https://api.openai.com/v1/threads/" + threadID + "/messages";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(messageURL))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);
            webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v1");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Use the setter of the MessagesThreadData property to store the response
                string _MessagesThreadData = webRequest.downloadHandler.text;
                // Debug.Log("Messages Thread Data: " + _MessagesThreadData);
                MessagesThreadResponse MessagesThreadResponseData = JsonUtility.FromJson<MessagesThreadResponse>(_MessagesThreadData);
                string chatbotResponse = MessagesThreadResponseData.data[0].content[0].text.value;
                string chatbotResponseID = MessagesThreadResponseData.data[0].id;
                // Debug.Log("Number of messages loaded: " + MessagesThreadResponseData.data.Count);
                // Debug.Log("Chatbot Response ID: " + chatbotResponseID);
                // Debug.Log("Chatbot Response: " + chatbotResponse);

                // Assuming jsonResponse is a JSON string that you've parsed
                // Extract messages from jsonResponse and store them in an array
                currentMessageIndex = 0;
                messages = ExtractMessages(chatbotResponse);
                UpdateMaxIndex();
                StartCoroutine(DisplayMessagesCoroutine(messages));
                // OnResponse.Invoke(chatbotResponse);
            }
        }
    }

    private string[] ExtractMessages(string jsonResponse)
    {
        // Implement JSON parsing here to extract messages based on your response structure
        JsonTextParser jsonTextParser = new JsonTextParser();
        return jsonTextParser.ParseJsonResponse(jsonResponse);
        // return new string[] { /* Messages extracted from jsonResponse */ };
    }

    private IEnumerator DisplayMessagesCoroutine(string[] messages)
    {
        currentChatBubble =  GameObject.Find("ChatBubble");
        foreach (var message in messages)
        {
            UpdateMessage(message);
            UpdateCurrentIndex();
            currentMessageIndex++;
            // Debug.Log("Individual Response: " + message);
            yield return new WaitForSeconds(3f); // Wait for 2 seconds before showing the next message
        }
    }

    private void UpdateMessage(string messageText)
    {
        // currentChatBubble = Instantiate(chatBubblePrefab, chatContentPanel);
        if (messageText != null)
        {
            TextMeshProUGUI bubbleText = currentChatBubble.GetComponentInChildren<TextMeshProUGUI>(); // or TextMeshProUGUI if using TextMeshPro
            bubbleText.text = messageText;
        }
        else
        {
            Debug.LogError("Chat bubble TextMeshPro component not found!");
        }
    }

    public void GetPreviousMessage()
    {
        // Implement logic to display previous messages
        if (currentMessageIndex >= 0)
        {
            currentMessageIndex--;
            UpdateMessage(messages[currentMessageIndex]);
            UpdateCurrentIndex();
        }

    }

    public void GetNextMessage(){
        // Implement logic to display previous messages
        if (currentMessageIndex < messages.Length)
        {
            currentMessageIndex++;
            UpdateMessage(messages[currentMessageIndex]);
            UpdateCurrentIndex();
        }

    }
    
    private void UpdateCurrentIndex(){
        // Implement logic to display max index
        if (messages != null)
        {
            var currentDisplayedIndex = currentMessageIndex + 1;
            currentIndex.text = currentDisplayedIndex.ToString();
        }
    }

    private void UpdateMaxIndex(){
        // Implement logic to display max index
        if (messages != null)
        {
            maxIndex.text = messages.Length.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
