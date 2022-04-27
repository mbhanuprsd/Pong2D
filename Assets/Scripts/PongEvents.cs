using System;
using UnityEngine;

public class PongEvents : MonoBehaviour
{
    public static PongEvents current;

    private void Awake()
    {
        current = this;
    }

    /// <summary>
    /// Event called when ball exits screen
    /// </summary>
    public event Action OnBallScreenExit;
    public event Action OnLifeLost;

    public void BallScreenExit()
    {
        OnBallScreenExit?.Invoke();
        OnLifeLost?.Invoke();
    }

    /// <summary>
    /// Event called when the ball hits the brick
    /// </summary>
    public event Action OnBrickHit;
    public event Action OnScoreUpdate;

    public void BrickHit()
    {
        OnBrickHit?.Invoke();
        OnScoreUpdate?.Invoke();
    }

    public event Action OnStartGame;
    public void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public event Action<bool> OnGameOver;
    public void GameOver(bool status)
    {
        OnGameOver?.Invoke(status);
    }
}
