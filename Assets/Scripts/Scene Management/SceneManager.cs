using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management functions
using AvatarManagement; // Required for AvatarData

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private void LoadNextScene()
    {
        int currentIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextIndex); // Load the next scene by index
        }
        else
        {
            Debug.LogWarning("No next scene available.");
        }
    }
}
