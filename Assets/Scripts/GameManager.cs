using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Button[] buttons; // Assign all 9 buttons in Inspector
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI[] buttonTexts; // Assign all 9 TMP text objects in Inspector
    public TextMeshProUGUI msgFeedback;

    private string[] board = new string[9];
    private string playerMark = "X";
    private string aiMark = "O";
    private bool gameOver = false;

    void Start()
    {
        ResetBoard();
    }

    public void PlayerMove(int index)
    {
        Debug.Log("Player clicked index: " + index);
        if (board[index] == "" && !gameOver)
        {
            MarkSquare(index, playerMark);
            if (CheckWin(playerMark))
            {
                resultText.text = "You Win!";
                GameSetting.wonTicTacToe = true;
                GameSetting.playerLives = 1; //gain one life back
                GameSetting.chances = 0;
                StartCoroutine(LoadMainSceneAfterDelay(1.5f));

                //gameOver = true;
                return;
            }
            if (IsBoardFull())
            {
                resultText.text = "Draw!";
                GameSetting.wonTicTacToe = false;
                //gameOver = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
                //return;
            }
            AIMove();
        }
    }

    void AIMove()
    {
        List<int> availableMoves = new List<int>();
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == "")
                availableMoves.Add(i);
        }

        if (availableMoves.Count > 0)
        {
            int randomIndex = availableMoves[Random.Range(0, availableMoves.Count)];
            MarkSquare(randomIndex, aiMark);
            if (CheckWin(aiMark))
            {
                resultText.text = "Computer Wins!";
                GameSetting.wonTicTacToe = false;
                //gameOver = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            }
            else if (IsBoardFull())
            {
                resultText.text = "Draw!";
                GameSetting.wonTicTacToe = false;
                //gameOver = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            }
        }
    }

    void MarkSquare(int index, string mark)
    {
        board[index] = mark;
        buttonTexts[index].text = mark;
        buttons[index].interactable = false;
    }

    bool CheckWin(string mark)
    {
        int[,] winConditions = new int[,] {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Rows
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Columns
            {0, 4, 8}, {2, 4, 6}             // Diagonals
        };

        for (int i = 0; i < winConditions.GetLength(0); i++)
        {
            if (board[winConditions[i, 0]] == mark &&
                board[winConditions[i, 1]] == mark &&
                board[winConditions[i, 2]] == mark)
                return true;
        }
        return false;
    }

    bool IsBoardFull()
    {
        foreach (string square in board)
        {
            if (square == "") return false;
        }
        return true;
    }

    public void ResetBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = "";
            buttonTexts[i].text = "";
            buttons[i].interactable = true;
        }

        resultText.text = "";
        gameOver = false;
    }

    public void ButtonPressed(int index)
    {
        PlayerMove(index);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    private IEnumerator LoadMainSceneAfterDelay(float delay)
    {
        if (GameSetting.level == 1)
        {
            yield return new WaitForSecondsRealtime(delay);  // unaffected by Time.timeScale
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainScene");
        }
        else if (GameSetting.level == 2)
        {
            yield return new WaitForSecondsRealtime(delay);  // unaffected by Time.timeScale
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainScene1");
        }
    }
}
