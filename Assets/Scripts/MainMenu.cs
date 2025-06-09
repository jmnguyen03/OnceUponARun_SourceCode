using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField maxScoreInput;
    public void StartGame()
    {
        GameSetting.wonTicTacToe = false;
        GameSetting.chances = 1;
        // Fallback value in case the field is empty or invalid
        int inputScore = 100; // fallback

        if (int.TryParse(maxScoreInput.text, out int parsedScore) && parsedScore > 0)
        {
            inputScore = parsedScore;
        }

        GameSetting.inputMaxScore = inputScore;
        GameSetting.level1ScoreGoal = Mathf.RoundToInt(inputScore * 0.7f);
        GameSetting.level2ScoreGoal = inputScore - GameSetting.level1ScoreGoal; // the remaining 30%

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

}
