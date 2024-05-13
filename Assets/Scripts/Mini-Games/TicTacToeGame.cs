using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TicTacToe
{
    public class TicTacToeGame : MonoBehaviour
    {
        public GameObject[] cells; // Assign the 9 grid cells in the Inspector
        public Sprite[] sprites; // Array to hold sprite variants for X and O
        private string[] board = new string[9]; // Represents the game board for minimax
        private bool isPlayerXTurn = true; // True if it's Player X's turn, false for AI
        private int[] WinningCells;
        private Image imageChild; // Reference to the Image component of the child object
        private bool hardAI = true;
        public GameObject dialogueBox;
        string[] messagePrompts = {
            "Take a deep breath and remember to be kind to yourself. You are enough.",
            "It's okay to ask for help. Reach out to someone you trust and share your feelings.",
            "Practice gratitude today. Reflect on three things you're grateful for in your life.",
            "You're not alone. Connect with others and share your experiences. You may find support in unexpected places.",
            "Embrace imperfection. Remember that growth happens outside your comfort zone.",
            "Set boundaries to protect your mental health. Saying no is an act of self-care.",
            "Focus on the present moment. Practice mindfulness to calm your mind and reduce stress.",
            "Celebrate small victories. Acknowledge your progress, no matter how small it may seem.",
            "You are resilient. Remember that setbacks are opportunities for growth and learning."
        };

        // public GameObject winMessage; // Assign the win message panel in the Inspector

        void Start()
        {
            RestartGame();
            // imageChild = cells[0].GetComponent<Image>().gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();
            // Debug.Log("Image child: " + imageChild.gameObject.name);
        }

        public void SetEasyAI(){
            Debug.Log("Set AI to easy");
            hardAI = false;
        }

        public void SetHardAI(){
            Debug.Log("Set AI to hard");
            hardAI = true;
        }

        // void StartGame()
        // {
        //     // Randomly decide who starts or set based on a user choice
        //     isPlayerXTurn = true; //Random.value > 0.5f;

        //     // if (!isPlayerXTurn)
        //     // {
        //     //     Debug.Log("AI starts first");
        //     //     AiTakeTurn(); // If AI goes first, make its move
        //     // }
        // }


        // Call this method when a cell is clicked
        public void CellClicked(int index)
        {
            Debug.Log("Cell " + index + " clicked!");
            Debug.Log("isPlayerXTurn: " + isPlayerXTurn);
            if (board[index] == "" && isPlayerXTurn)
            {
                UpdateCell(index, "X");
                isPlayerXTurn = !isPlayerXTurn; // Switch turns
                AiTakeTurn();
            }
        }

        // Update the cell and internal board representation
        void UpdateCell(int index, string player)
        {
            cells[index].GetComponentInChildren<Text>().text = player;
            // Find the Image component attached to the child object
            imageChild = cells[index].GetComponent<Image>().gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            // Debug.Log("Image child: " + imageChild.gameObject.name);
            if (player == "X")
            {
                imageChild.sprite = sprites[0];
            }
            else if (player == "O")
            {
                imageChild.sprite = sprites[1];
            }
            imageChild.gameObject.SetActive(true);
            board[index] = player;
            ShowPrompt();
            CheckForGameOver();
        }

        public void ShowPrompt(){
            // show random prompt
            int randomIndex = Random.Range(0, messagePrompts.Length);
            dialogueBox.GetComponentInChildren<TMP_Text>().text = messagePrompts[randomIndex];
            dialogueBox.SetActive(true);
        }

        public void ClosePrompt(){
            dialogueBox.SetActive(false);
        }

        void AiTakeTurn()
        {
            Debug.Log("AI's turn now!");
            bool gameOver = CheckForGameOver();
            if(!gameOver)
            {
                int immediateThreatIndex = -1;
                if(hardAI){
                    immediateThreatIndex = CheckForImmediateThreat(board, "X"); // Assuming "X" is the opponent's marker
                }
                else
                {
                    immediateThreatIndex = -1;
                }
                if (immediateThreatIndex != -1)
                {
                    // Block the immediate threat
                    board[immediateThreatIndex] = "O"; // Assuming "O" is the AI's marker
                    UpdateCell(immediateThreatIndex, "O"); // Update the game board UI
                    isPlayerXTurn = !isPlayerXTurn; // Switch turns back to player
                }
                else
                {
                    int bestScore = int.MinValue;
                    int move = -1;
                    for (int i = 0; i < board.Length; i++)
                    {
                        if (board[i] == "")
                        {
                            board[i] = "O";
                            // int score = Minimax(board, 0, true, maxDepth: 5);
                            int score = Minimax(board, 0, true, int.MinValue, int.MaxValue);
                            board[i] = "";
                            if (score > bestScore)
                            {
                                bestScore = score;
                                move = i;
                            }
                        }
                    }
                    // Debug.Log("Score: " + bestScore);
                    Debug.Log("Best move: " + move);

                    if (move != -1)
                    {
                        Debug.Log("AI chose cell " + move);
                        UpdateCell(move, "O");
                        board[move] = "O";
                        isPlayerXTurn = !isPlayerXTurn; // Switch turns back to player
                    }
                    else
                    {
                        // No valid moves found
                        Debug.Log("No valid moves for AI. Checking game state...");
                        CheckForGameOver();
                    }
                }
            }
        }
        
        //check for imminent threats
        int CheckForImmediateThreat(string[] board, string opponentMarker)
        {
            // Rows
            for (int row = 0; row < 3; row++)
            {
                int rowSum = 0;
                int emptyIndex = -1;
                for (int col = 0; col < 3; col++)
                {
                    int index = row * 3 + col;
                    if (board[index] == opponentMarker) rowSum++;
                    else if (board[index] == "") emptyIndex = index;

                    if (rowSum == 2 && emptyIndex != -1) return emptyIndex;
                }
            }

            // Columns
            for (int col = 0; col < 3; col++)
            {
                int colSum = 0;
                int emptyIndex = -1;
                for (int row = 0; row < 3; row++)
                {
                    int index = row * 3 + col;
                    if (board[index] == opponentMarker) colSum++;
                    else if (board[index] == "") emptyIndex = index;

                    if (colSum == 2 && emptyIndex != -1) return emptyIndex;
                }
            }

            // Diagonals
            int diagSum1 = 0, diagSum2 = 0;
            int emptyIndexDiag1 = -1, emptyIndexDiag2 = -1;
            for (int i = 0; i < 3; i++)
            {
                // Primary diagonal
                int indexDiag1 = i * 3 + i;
                if (board[indexDiag1] == opponentMarker) diagSum1++;
                else if (board[indexDiag1] == "") emptyIndexDiag1 = indexDiag1;

                // Secondary diagonal
                int indexDiag2 = i * 3 + (2 - i);
                if (board[indexDiag2] == opponentMarker) diagSum2++;
                else if (board[indexDiag2] == "") emptyIndexDiag2 = indexDiag2;
            }
            if (diagSum1 == 2 && emptyIndexDiag1 != -1) return emptyIndexDiag1;
            if (diagSum2 == 2 && emptyIndexDiag2 != -1) return emptyIndexDiag2;

            // No immediate threat found
            return -1;
        }


        // alpha-beta pruning
        int Minimax(string[] board, int depth, bool isMaximizing, int alpha, int beta)
        {
            string result = CheckWinner();
            if (result != null) 
            { /* Terminal state scoring */ 
                if (result == "O") return 10 - depth; // AI win, less depth is better
                else if (result == "X") return  depth - 10; // Player win, more depth is better
                else return 0; // Draw
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == "")
                    {
                        board[i] = "O";
                        int score = Minimax(board, depth + 1, false, alpha, beta);
                        board[i] = "";
                        bestScore = Mathf.Max(score, bestScore);
                        alpha = Mathf.Max(alpha, score);
                        if (beta <= alpha) break; // Alpha-beta pruning
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == "")
                    {
                        board[i] = "X";
                        int score = Minimax(board, depth + 1, true, alpha, beta);
                        board[i] = "";
                        bestScore = Mathf.Min(score, bestScore);
                        beta = Mathf.Min(beta, score);
                        if (beta <= alpha) break; // Alpha-beta pruning
                    }
                }
                return bestScore;
            }
        }



        bool CheckForGameOver()
        {
            string winner = CheckWinner();
            if (winner != null)
            {
                // Handle game over: display winner and restart game
                Debug.Log("Game Over. Winner: " + winner);
                HighlightWinningCells(WinningCells);
                return true;
                // RestartGame();
            }
            else if (IsBoardFull())
            {
                // Handle draw
                Debug.Log("Game Over. It's a draw!");
                return true;
                // RestartGame();
            }
            return false;
        }

        string CheckWinner()
        {
            // Check for a winning combination on the board
            // Return "X", "O", or null for no winner yet
            // Implement your win condition check here based on the 'board' array
            for (int i = 0; i < 3; i++)
            {
                // // Check for column victory
                // if (cells[i%9].GetComponentInChildren<Text>().text == "X" && cells[i%9].GetComponentInChildren<Text>().text == cells[(i + 3)%9].GetComponentInChildren<Text>().text && cells[(i + 3)%9].GetComponentInChildren<Text>().text == cells[(i + 6)%9].GetComponentInChildren<Text>().text)
                // {
                //     Debug.Log("Player X wins!");
                //     return "X";
                // }
                // else if (cells[i%9].GetComponentInChildren<Text>().text == "O" && cells[i%9].GetComponentInChildren<Text>().text == cells[(i + 3)%9].GetComponentInChildren<Text>().text && cells[(i + 3)%9].GetComponentInChildren<Text>().text == cells[(i + 6)%9].GetComponentInChildren<Text>().text)
                // {
                //     Debug.Log("Player O wins!");
                //     return "O";
                // }
                // // Check for row victory
                // else if (cells[i*3].GetComponentInChildren<Text>().text == "X" && cells[i*3].GetComponentInChildren<Text>().text == cells[(i*3 + 1)%9].GetComponentInChildren<Text>().text && cells[(i*3 + 1)%9].GetComponentInChildren<Text>().text == cells[(i*3 + 2)%9].GetComponentInChildren<Text>().text)
                // {
                //     Debug.Log("Player X wins!");
                //     return "X";
                // }
                // else if (cells[i*3].GetComponentInChildren<Text>().text == "O" && cells[i*3].GetComponentInChildren<Text>().text == cells[(i*3 + 1)%9].GetComponentInChildren<Text>().text && cells[(i*3 + 1)%9].GetComponentInChildren<Text>().text == cells[(i*3 + 2)%9].GetComponentInChildren<Text>().text)
                // {
                //     Debug.Log("Player O wins!");
                //     return "O";
                // }
                // Check rows
                if (cells[i * 3].GetComponentInChildren<Text>().text != "" &&
                    cells[i * 3].GetComponentInChildren<Text>().text == cells[i * 3 + 1].GetComponentInChildren<Text>().text &&
                    cells[i * 3 + 1].GetComponentInChildren<Text>().text == cells[i * 3 + 2].GetComponentInChildren<Text>().text)
                {
                    // return new WinResult { Winner = cells[i * 3].GetComponentInChildren<Text>().text, WinningCells = new int[] { i * 3, i * 3 + 1, i * 3 + 2 } };
                    WinningCells = new int[] { i * 3, i * 3 + 1, i * 3 + 2 };
                    return cells[i * 3].GetComponentInChildren<Text>().text;
                }

                // Check columns
                if (cells[i].GetComponentInChildren<Text>().text != "" &&
                    cells[i].GetComponentInChildren<Text>().text == cells[i + 3].GetComponentInChildren<Text>().text &&
                    cells[i + 3].GetComponentInChildren<Text>().text == cells[i + 6].GetComponentInChildren<Text>().text)
                {
                    WinningCells = new int[] { i, i + 3, i + 6 };
                    return cells[i].GetComponentInChildren<Text>().text;
                    // return new WinResult { Winner = cells[i].GetComponentInChildren<Text>().text, WinningCells = new int[] { i, i + 3, i + 6 } };
                }

                // Check for diagonal victory
                // Diagonal check (top-left to bottom-right)
                if (cells[0].GetComponentInChildren<Text>().text != "" &&
                    cells[0].GetComponentInChildren<Text>().text == cells[4].GetComponentInChildren<Text>().text &&
                    cells[4].GetComponentInChildren<Text>().text == cells[8].GetComponentInChildren<Text>().text)
                {
                    // return cells[0].GetComponentInChildren<Text>().text;
                    WinningCells = new int[] { 0, 4, 8 };
                    return cells[0].GetComponentInChildren<Text>().text;
                    // return new WinResult { Winner = cells[0].GetComponentInChildren<Text>().text, WinningCells = new int[] { 0, 4, 8 } };
                }

                // Diagonal check (top-right to bottom-left)
                if (cells[2].GetComponentInChildren<Text>().text != "" &&
                    cells[2].GetComponentInChildren<Text>().text == cells[4].GetComponentInChildren<Text>().text &&
                    cells[4].GetComponentInChildren<Text>().text == cells[6].GetComponentInChildren<Text>().text)
                {
                    WinningCells = new int[] { 2, 4, 6 };
                    return cells[2].GetComponentInChildren<Text>().text;
                    // return new WinResult { Winner = cells[2].GetComponentInChildren<Text>().text, WinningCells = new int[] { 2, 4, 6 } };
                }
            }
            // No winner yet
            return null;
        }

        bool IsBoardFull()
        {
            foreach (string spot in board)
            {
                if (spot == "") return false;
            }
            return true;
        }

        void HighlightWinningCells(int[] winningCells)
        {
            foreach (int index in winningCells)
            {
                // Assuming your cells are UI Buttons with an Image component
                cells[index].GetComponent<Image>().color = Color.green;
            }
        }

        public void RestartGame()
        {
            // Reset the game board and UI
            // Debug.Log("Restarting game...");
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = "";
                imageChild = cells[i].GetComponent<Image>().gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
                imageChild.gameObject.SetActive(false);
                imageChild.color = Color.white; // Reset color
                imageChild.sprite = null; // Reset sprite
                cells[i].GetComponentInChildren<Text>().text = "";
                cells[i].GetComponent<Image>().color = Color.white;
            }
            isPlayerXTurn = true; // Or randomize starting player
        }
    }
}