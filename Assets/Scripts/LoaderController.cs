using UnityEngine;
using UnityEngine.UI;

public class LoaderController : MonoBehaviour
{
    public GameObject loaderPanel; // Assign the Panel you created in the Inspector
    public Image spinnerImage; // Assign if you have a spinner Image

    void Update()
    {
        // Optional: Spin the loader image
        if (spinnerImage != null)
        {
            spinnerImage.transform.Rotate(0, 0, -100 * Time.deltaTime);
        }
    }

    public void ShowLoader()
    {
        loaderPanel.SetActive(true);
    }

    public void HideLoader()
    {
        loaderPanel.SetActive(false);
    }
}
