using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    LevelSetup levelSetup;

    private const int MaxLives = 3;

    public int Lives = MaxLives;
    public int Score = 0;
    public int Level = 1;

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

    private void UpdateUI(GAME_STATE gameState)
    {
        switch(gameState)
        {
            case GAME_STATE.INITIAL:
                gameObject.SetActive(true);
                Lives = 3;
                break;
            case GAME_STATE.IN_GAME:
                gameObject.SetActive(false);
                break;
            case GAME_STATE.GAMEOVER:
                gameObject.SetActive(true);
                levelSetup.DisableLevel();
                Lives = 3;
                break;
            default:
                break;
        }
    }
}
