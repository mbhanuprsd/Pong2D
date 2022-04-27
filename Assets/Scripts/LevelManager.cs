using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject ballPrefab, wallPrefab, playerPrefab, brickPrefab;

    private static List<GameObject> bricks = new List<GameObject>();
    private List<GameObject> pongElements = new List<GameObject>();

    [SerializeField]
    int numberOfBrickLayers = 2;

    private float ScreenWidth, ScreenHeight;

    /// <summary>
    /// Update the bricks and visibility of elements
    /// </summary>
    private void Start()
    {
        ScreenWidth = (float)(Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height);
        ScreenHeight = (float)(Camera.main.orthographicSize * 2.0f);

        PongEvents.current.OnStartGame += SpawnLevel;
        PongEvents.current.OnGameOver += DisableLevel;
    }

    /// <summary>
    /// Spawns the elements in th elevel and intializes ball physics
    /// </summary>
    public void SpawnLevel()
    {
        InitializeElements();
        SpawnBricks();
    }

    /// <summary>
    /// Updates the ball state and elements on game over
    /// </summary>
    public void DisableLevel(bool status)
    {
        foreach (GameObject pongElement in pongElements)
        {
            Destroy(pongElement);
        }

        foreach (GameObject brick in bricks)
        {
            Destroy(brick);
        }
    }

    public static int GetBrickCount()
    {
        return bricks.Count;
    }

    /// <summary>
    /// Spawns ball, walls and player
    /// </summary>
    private void InitializeElements()
    {
        GameObject ball = Instantiate(ballPrefab);
        pongElements.Add(ball);

        GameObject leftWall = Instantiate(wallPrefab);
        leftWall.GetComponent<BoundaryController>().WallSide = BoundaryController.SIDE.LEFT;

        GameObject rightWall = Instantiate(wallPrefab);
        rightWall.GetComponent<BoundaryController>().WallSide = BoundaryController.SIDE.RIGHT;

        GameObject topWall = Instantiate(wallPrefab);
        topWall.GetComponent<BoundaryController>().WallSide = BoundaryController.SIDE.TOP;

        GameObject bottomWall = Instantiate(wallPrefab);
        bottomWall.GetComponent<BoundaryController>().WallSide = BoundaryController.SIDE.BOTTOM;

        pongElements.Add(leftWall);
        pongElements.Add(rightWall);
        pongElements.Add(topWall);
        pongElements.Add(bottomWall);

        GameObject player = Instantiate(playerPrefab);
        pongElements.Add(player);
    }

    /// <summary>
    /// Spawn bricks based o number of layers and screen width
    /// </summary>
    private void SpawnBricks()
    {
        var blockWidth = 1 / 8.0f;
        var blockHeight = 1 / 32.0f;
        Vector3 brickScale = new(ScreenWidth / 8.0f, ScreenHeight / 32.0f, 1);
        var blockPadding = 1 / 80.0f;
        var i = 0;
        var j = numberOfBrickLayers;
        while (j > 0)
        {
            var start = i * (blockWidth + blockPadding) + blockPadding;
            var end = 1.0f - start - blockPadding;
            var blockXCoord = start;
            var blockYCoord = i * (blockHeight + blockPadding) + blockPadding;
            j--;
            while (end > blockXCoord + blockWidth)
            {
                Vector3 brickPosition = Camera.main.ViewportToWorldPoint(
                    new Vector3(
                       1 - blockXCoord - blockWidth / 2.0f - blockPadding,
                       0.9f - blockHeight - blockYCoord,
                        Camera.main.nearClipPlane)
                    );
                GameObject newBrick = Instantiate(brickPrefab, brickPosition, Quaternion.identity);
                newBrick.transform.localScale = brickScale;
                bricks.Add(newBrick);
                blockXCoord += (blockWidth + blockPadding);
            }
            i++;
        }
    }
}
