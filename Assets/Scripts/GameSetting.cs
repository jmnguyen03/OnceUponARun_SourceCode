using UnityEngine;

public static class GameSetting
{
    public static int maxScore = 100; // Set from Main Menu

    public static int inputMaxScore = 200; // Set from Main Menu
    public static int level1ScoreGoal = Mathf.RoundToInt(inputMaxScore * 0.7f);
    public static int level2ScoreGoal = inputMaxScore - GameSetting.level1ScoreGoal;


    // For Main Scene → Tic Tac Toe → Main Scene
    public static int playerLives = 2;
    public static int currentScore = 0;
    public static bool wonTicTacToe = false;
    public static int chances = 1;

    public static int level = 1;
    public static bool keyCollected = false;


    //points system
    public static int aloePoint = 2;
}
