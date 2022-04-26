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

    /// <summary>
    /// Setup the level to play
    /// </summary>
    public void StartGame()
    {
        UpdateUI(GAME_STATE.IN_GAME);
        levelSetup.SpawnLevel();
    }

    /// <summary>
    /// Update lives and check game over condition if lives become zero
    /// </summary>
    private void UpdateLives()
    {
        if (Lives > 1)
            Lives--;
        else
        {
            UpdateUI(GAME_STATE.GAMEOVER);
        }
    }

    /// <summary>
    /// Update score and check game over condition if completed breaking all bricks
    /// </summary>
    private void UpdateScore()
    {
        Score++;
        scoreText.text = "Score: " + Score;

        if (Score == levelSetup.GetBrickCount())
        {
            UpdateUI(GAME_STATE.GAMEOVER);
        }
    }

    /// <summary>
    /// Update the UI based on the game state
    /// </summary>
    /// <param name="gameState">Status of game</param>
    private void UpdateUI(GAME_STATE gameState)
    {
        switch(gameState)
        {
            case GAME_STATE.INITIAL:
                MainCanvas.SetActive(true);
                InGameCanvas.SetActive(false);
                Lives = MaxLives;
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
                StartCoroutine(DelayGameOver(3.0f));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    IEnumerator DelayGameOver(float delayTime)
    {
        playerWonText.text = Score == levelSetup.GetBrickCount() ? "You Won!" : "You Lost!";
        levelSetup.DisableLevel();
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        MainCanvas.SetActive(true);
        InGameCanvas.SetActive(false);
        Lives = MaxLives;
    }

    internal static int GetScore()
    {
        return Score;
    }
}
