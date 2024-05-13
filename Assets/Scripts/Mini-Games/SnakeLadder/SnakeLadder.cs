using UnityEngine;

public class SnakesAndLadders : MonoBehaviour
{
    public int[] board = new int[100]; // Represents the game board with 100 squares
    public int playerPosition = 0; // Player's starting position

    // Start is called before the first frame update
    void Start()
    {
        InitializeBoard();
    }

    void InitializeBoard()
    {
        // Initialize board, ladders, and snakes
        // Example: Ladder from square 2 to square 23
        board[2] = 23 - 2; // Positive value for ladder

        // Example: Snake from square 95 to square 75
        board[95] = 75 - 95; // Negative value for snake

        // Add more snakes and ladders as needed
    }

    public void MovePlayer(int diceRoll)
    {
        playerPosition += diceRoll; // Move player based on dice roll

        // Check for landing on snake or ladder
        if (board[playerPosition] != 0)
        {
            playerPosition += board[playerPosition]; // Move to new position based on snake or ladder
        }

        // Ensure playerPosition is within bounds
        playerPosition = Mathf.Clamp(playerPosition, 0, 99);

        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (playerPosition >= 99) // Adjust based on your game's win condition
        {
            Debug.Log("Player Wins!");
            // Implement win condition actions (e.g., restart game, display win message)
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Roll dice on space bar press
        {
            int diceRoll = Random.Range(1, 7); // Simulate dice roll (1-6)
            MovePlayer(diceRoll);
        }
    }
}
