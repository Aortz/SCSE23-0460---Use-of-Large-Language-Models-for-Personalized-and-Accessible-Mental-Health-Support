using UnityEngine;
using UnityEngine.UI; // Include this for UI elements

public class ObjectDoubleClick : MonoBehaviour
{
    public GameObject gameOptionsMenu;
    public GameObject ticTacToeBoard; // Assign the game board panel in the Inspector
    public GameObject snakeLadderBoard; // Assign the game board panel in the Inspector
    public GameObject pressXIndicator;    // Assign a UI Text or similar in Inspector
    public float activationDistance = 1.0f; // Distance within which the player can activate the menu
    private float catchTime = 0.4f; // Time in seconds between clicks to be considered a double click
    private float lastClickTime = 0f;

    private GameObject player; // To store a reference to the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player is tagged as "Player"
        pressXIndicator.SetActive(false); // Initially hide the "Press X" indicator
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= activationDistance)
        {
            // Show "Press X" indicator when within range
            pressXIndicator.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                float clickTime = Time.time;
                // Check if the current click is within the catch time of the last click
                if ((clickTime - lastClickTime) < catchTime)
                {
                    // Toggle the panel's visibility
                    OpenGameSelectionMenu();
                }
                lastClickTime = clickTime;
            }

            // // Check for 'X' or 'C' key press to toggle game selection panel
            // if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C))
            // {
            //     // Toggle the panel's visibility
            //     OpenGameSelectionMenu();
            // }
        }
        else
        {
            // Hide "Press X" indicator and game selection panel when out of range
            pressXIndicator.SetActive(false);
            gameOptionsMenu.SetActive(false);
        }
    }

    void OpenGameSelectionMenu()
    {
        // Code to open the game selection menu
        gameOptionsMenu.SetActive(!gameOptionsMenu.activeSelf);
    }

    public void StartTicTacToe()
    {
        // Code to start the Tic Tac Toe game
        ticTacToeBoard.SetActive(true);
        gameOptionsMenu.SetActive(false);
    }

    public void EndTicTacToe()
    {
        // Code to restart the game
        ticTacToeBoard.SetActive(false);
        gameOptionsMenu.SetActive(true);
    }

    public void StartSnakeAndLadders()
    {
        // Code to start the Tic Tac Toe game
        snakeLadderBoard.SetActive(true);
        gameOptionsMenu.SetActive(false);
    }

    public void EndSnakeAndLadders()
    {
        // Code to restart the game
        snakeLadderBoard.SetActive(false);
        gameOptionsMenu.SetActive(true);
    }

    public void CloseGameSelectionMenu()
    {
        // Code to close the game selection menu
        gameOptionsMenu.SetActive(false);
    }
}
