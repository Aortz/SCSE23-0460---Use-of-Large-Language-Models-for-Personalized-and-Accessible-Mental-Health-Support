using UnityEngine;
using UnityEngine.UI;

public class GenerateGrid : MonoBehaviour
{
    public GameObject squarePrefab; // Assign in the inspector
    public int rows = 10;
    public int columns = 10;
    public float squareSpacing = 105f; // Adjust based on your prefab's size and desired spacing

    private RectTransform canvasRectTransform;

    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject square = Instantiate(squarePrefab, transform);
                square.name = $"Square {x + y * columns}";
                square.transform.localPosition = new Vector3(x * squareSpacing, -y * squareSpacing, 0);
            }
        }

        // Center the board
        float boardWidth = columns * squareSpacing;
        float boardHeight = rows * squareSpacing;
        transform.localPosition = new Vector3(-boardWidth / 2 + squareSpacing / 2, boardHeight / 2 - squareSpacing / 2, 0);
    }
}
