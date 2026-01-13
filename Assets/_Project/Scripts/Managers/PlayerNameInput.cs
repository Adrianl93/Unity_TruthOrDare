using TMPro;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text label;

    private string defaultName;

    public string PlayerName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(inputField.text))
                return defaultName;

            return inputField.text.Trim();
        }
    }

    public void SetIndex(int index)
    {
        defaultName = $"Player {index}";
        label.text = defaultName;

        // Placeholder visual
        if (inputField.placeholder is TMP_Text placeholderText)
        {
            placeholderText.text = defaultName;
        }
    }
}
