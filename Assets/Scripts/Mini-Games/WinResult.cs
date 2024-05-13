using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class WinResult : MonoBehaviour
    {
        // public GameObject winMessage; // Assign the win message panel in the Inspector
        public string Winner;
        public int[] WinningCells;
    }
}