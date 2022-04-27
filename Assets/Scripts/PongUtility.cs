using UnityEngine;

public class PongUtility : MonoBehaviour
{
    public static int MaxLives = 3, Lives = 0, Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        PongEvents.current.OnStartGame += StartGame;
        PongEvents.current.OnBrickHit += UpdateScore;
        PongEvents.current.OnBallScreenExit += UpdateLives;
    }

    /// <summary>
    /// Setup the level to play
    /// </summary>
    public void StartGame()
    {
        Lives = MaxLives;
        Score = 0;
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
           PongEvents.current.GameOver(false);
        }
    }

    /// <summary>
    /// Update score and check game over condition if completed breaking all bricks
    /// </summary>
    private void UpdateScore()
    {
        Score++;
        if(Score == LevelManager.GetBrickCount())
        {
            PongEvents.current.GameOver(true);
        }
    }

    public struct ScreenResolution
    {
        public float Width, Height;

        public ScreenResolution(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }

    public static ScreenResolution GetScreenResolution()
    {
        float ScreenWidth = (float)(Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height);
        float ScreenHeight = (float)(Camera.main.orthographicSize * 2.0f);
        return new ScreenResolution(ScreenWidth, ScreenHeight);
    }

    public static Vector2 CreateRandomForce(int forceIntensity)
    {
        float randomX = Random.Range(-1.0f, -0.5f);
        float randomY = Random.Range(0.5f, 1.0f);
        Vector2 randomForce = new(randomX, randomY);
        randomForce.Normalize();
        randomForce *= forceIntensity;
        return randomForce;
    }
}
