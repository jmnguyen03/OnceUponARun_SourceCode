using UnityEngine;
using UnityEngine.UI; // Required for Button component

public class RestartButtonHandler : MonoBehaviour
{
    // Reference to the GameManager
    public MainGameManager gameManager;

    void Start()
    {
        // Ensure the GameManager is assigned in the Inspector
        if (gameManager == null)
        {
            Debug.LogError("GameManager not assigned in RestartButtonHandler.");
        }

        // Get the Button component and set the OnClick event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnRestartButtonClicked);
        }
    }

    // Called when the button is clicked
    private void OnRestartButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.RestartGame();  // Call the RestartGame method from GameManager
        }
    }
}
