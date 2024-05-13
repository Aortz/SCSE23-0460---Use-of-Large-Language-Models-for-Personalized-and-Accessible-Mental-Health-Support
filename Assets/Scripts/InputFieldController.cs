using UnityEngine;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI placeholderText;

    private void Start()
    {
        inputField.onValueChanged.AddListener(ClearPlaceholder);
    }

    private void ClearPlaceholder(string text)
    {
        if (placeholderText != null)
        {
            placeholderText.text = "";
        }
    }
}
