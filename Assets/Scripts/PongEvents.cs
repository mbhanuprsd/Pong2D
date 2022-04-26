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

    public void BallScreenExit()
    {
        OnBallScreenExit?.Invoke();
    }

    /// <summary>
    /// Event called when the ball hits the brick
    /// </summary>
    public event Action OnBrickHit;

    public void BrickHit()
    {
        OnBrickHit?.Invoke();
    }
}
