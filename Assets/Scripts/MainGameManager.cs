using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainGameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public LifeDisplay lifeDisplay;

    public int lives = 2;
    private bool keyCollected = false;
    private string levelName;

    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;

        string scene = SceneManager.GetActiveScene().name;

        if (scene == "MainScene")
        {
            GameSetting.level = 1;
            keyCollected = false;
            GameSetting.maxScore = GameSetting.level1ScoreGoal;
        }
        else if (scene == "MainScene1")
        {
            GameSetting.level = 2;
            GameSetting.maxScore = GameSetting.level2ScoreGoal;
        }

        if (GameSetting.wonTicTacToe)
        {
            lives = GameSetting.playerLives;
            ScoreManager.instance.score = GameSetting.playerLives + GameSetting.currentScore;
            lifeDisplay.UpdateHearts(lives);
        }
        else
        {
            lives = 2;
            ScoreManager.instance.ResetScore();
        }

    }

    void Update()
    {
        // Check if player hits max score (key collected)
        if (!keyCollected && ScoreManager.instance.score >= GameSetting.maxScore)
        {
            keyCollected = true;
            Debug.Log("Key collected!");

            GameSetting.playerLives = lives;
            GameSetting.currentScore = ScoreManager.instance.score;
            GameSetting.wonTicTacToe = false;

            if (levelName == "MainScene")
            {
                GameSetting.playerLives = lives;
                Time.timeScale = 0f;
                GameSetting.currentScore = ScoreManager.instance.score;
                GameSetting.wonTicTacToe = false;
                StartCoroutine(LoadNextSceneAfterDelay("MainScene1", 1f)); // Go to level 2
            }
            else if (levelName == "MainScene1")
            {
                GameSetting.level = 2;
                Time.timeScale = 0f;
                StartCoroutine(LoadNextSceneAfterDelay("WinScene", 1f)); // Go to win screen
            }
        }
    }

    IEnumerator LoadNextSceneAfterDelay(string nextSceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }

    public void GameOver()
    {
        lives = 0;
        Debug.Log("Game Over triggered!");

        GameSetting.playerLives = lives;
        GameSetting.currentScore = ScoreManager.instance.score;
        GameSetting.wonTicTacToe = false;

        if (GameSetting.chances == 1)
        {
            SceneManager.LoadScene("SampleScene"); // Tic Tac Toe
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        Time.timeScale = 1f;
        lives = 2;
        GameSetting.playerLives = lives;  // Reset static value
        ScoreManager.instance.ResetScore();
        keyCollected = false;
        GameSetting.currentScore = 0;
        GameSetting.level = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoseLife()
    {
        lives--;
        lifeDisplay.UpdateHearts(lives);

        if (lives <= 0)
        {
            Time.timeScale = 0f;
            GameOver();
        }
    }

    public void GainLife()
    {
        if (lives <= 0)
        {
            lives = 1;
            lifeDisplay.UpdateHearts(lives);
        }
    }
}
