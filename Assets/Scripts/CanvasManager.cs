using System;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text gameTitle, scoreText, livesText;
    [SerializeField]
    UnityEngine.UI.Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        PongEvents.current.OnStartGame += InGame;
        PongEvents.current.OnGameOver += GameOver;
        PongEvents.current.OnScoreUpdate += UpdateScore;
        PongEvents.current.OnLifeLost += UpdateLives;
    }

    private void InGame()
    {
        scoreText.text = "Score : 0";
        livesText.text = new string('*', PongUtility.MaxLives);

        gameTitle.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
    }

    private void GameOver(bool status)
    {
        gameTitle.text = status ? "You Won!" : "You Lost!";
        startButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Play Again!";
        
        gameTitle.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        livesText.gameObject.SetActive(false);
    }

    private void UpdateScore()
    {
        scoreText.text = "Score : " + PongUtility.Score;
    }

    private void UpdateLives()
    {
        livesText.text = new string('*', PongUtility.Lives);
    }
}
