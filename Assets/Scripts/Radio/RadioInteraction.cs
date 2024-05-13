using UnityEngine;

public class RadioInteraction : MonoBehaviour
{
    // Add your code here
    private AudioSource audioSource;
    private bool isPlaying = false;
    private float lastClickTime = 0f;
    private float catchTime = 0.4f; // Time in seconds between clicks to be considered a double click
    public GameObject doubleClickIndicator;    // Assign a UI Text or similar in Inspector
    public float activationDistance = 2.0f; // Distance within which the player can activate the menu
    private GameObject player; // To store a reference to the player

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player is tagged as "Player"
        doubleClickIndicator.SetActive(false); // Initially hide the "Press X" indicator
    }

    void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            float clickTime = Time.time;
            // Check if the current click is within the catch time of the last click
            if ((clickTime - lastClickTime) < catchTime)
            {
                // Double click detected
                ToggleAudio();
            }
            lastClickTime = clickTime;
        }

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= activationDistance)
        {
            // Show "Double Click" indicator when within range
            doubleClickIndicator.SetActive(true);
        }
        else
        {
            // Hide "Double Click" indicator when out of range
            doubleClickIndicator.SetActive(false);
            audioSource.Pause();
        }
    }

    void ToggleAudio()
    {
        if (!isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
            ShowPrompt();
        }
        else
        {
            audioSource.Pause();
            isPlaying = false;
        }
    }

    void ShowPrompt()
    {
        // Code to display prompt (e.g., using Unity UI)
        Debug.Log("Would you like to start the mindfulness exercise?");
    }
}