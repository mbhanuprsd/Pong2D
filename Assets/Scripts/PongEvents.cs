using System;
using UnityEngine;

public class PongEvents : MonoBehaviour
{
    public static PongEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnBallScreenExit;

    public void BallScreenExit()
    {
        OnBallScreenExit?.Invoke();
    }
}
