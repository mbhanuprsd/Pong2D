using System;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    LevelSetup levelSetup;

    [SerializeField]
    GameObject MainCanvas, InGameCanvas;

    [SerializeField]
    TMPro.TextMeshProUGUI scoreText, playerWonText;

    private const int MaxLives = 3;

    private int Lives = MaxLives;
    private static int Score = 0;

    enum GAME_STATE
    {
        INITIAL,
        IN_GAME,
        GAMEOVER
    }

    // Start is called before the first frame update
    void Start()
    {
        PongEvents.current.OnBallScreenExit += UpdateLives;
        PongEvents.current.OnBrickHit += UpdateScore;
    }

    public void StartGame()
    {
        UpdateUI(GAME_STATE.IN_GAME);
        levelSetup.SpawnLevel();
    }

    private void UpdateLives()
    {
        if (Lives > 1)
            Lives--;
        else
        {
            UpdateUI(GAME_STATE.GAMEOVER);
            Lives = MaxLives;
        }
    }

    private void UpdateScore()
    {
        Score++;
        scoreText.text = "Score: " + Score;

        if (Score == levelSetup.GetBrickCount())
        {
            UpdateUI(GAME_STATE.GAMEOVER);
        }
    }

    private void UpdateUI(GAME_STATE gameState)
    {
        switch(gameState)
        {
            case GAME_STATE.INITIAL:
                MainCanvas.SetActive(true);
                InGameCanvas.SetActive(false);
                Lives = 3;
                Score = 0;
                break;
            case GAME_STATE.IN_GAME:
                MainCanvas.SetActive(false);
                InGameCanvas.SetActive(true);
                Score = 0;
                scoreText.text = "Score: " + Score;
                playerWonText.text = "";
                break;
            case GAME_STATE.GAMEOVER:
                playerWonText.text = Score == levelSetup.GetBrickCount() ? "You Won!" : "You Lost!";
                StartCoroutine(DelayGameOver(3.0f));
                break;
            default:
                break;
        }
    }

    IEnumerator DelayGameOver(float delayTime)
    {
        levelSetup.DisableLevel();
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        MainCanvas.SetActive(true);
        InGameCanvas.SetActive(false);
        Lives = 3;
    }

    internal static int GetScore()
    {
        return Score;
    }
}
