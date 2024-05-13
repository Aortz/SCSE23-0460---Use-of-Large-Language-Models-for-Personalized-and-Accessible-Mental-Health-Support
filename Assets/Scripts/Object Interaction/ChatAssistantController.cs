using UnityEngine;
using UnityEngine.UI; // Include this for UI elements
using UnityEngine.EventSystems; // Include this for EventSystem

public class ChatAssistantController : MonoBehaviour
{
    public GameObject chatAssistantPanel; // Assign the chat assistant panel in the Inspector

    public void toggleAssistantWindow()
    {
        // Code to start the Tic Tac Toe game
        EventSystem.current.SetSelectedGameObject(null);
        chatAssistantPanel.SetActive(!chatAssistantPanel.activeSelf);
    }
}