using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text TartgetScoreText;
    public MainGameManager gameManager;

    void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this to add to the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Call this to reset the score (for when restarting)
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Update the text display to reflect the current score
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        TartgetScoreText.text = "Target Score: " + GameSetting.maxScore;
    }

    // Show or hide the score text (for Game Over screen)
    public void SetScoreVisibility(bool isVisible)
    {
        //scoreText.gameObject.SetActive(isVisible);
    }
}
