using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [SerializeField]
    GameObject ball, topWall, leftWall, rightWall, player, brickPrefab;

    private List<GameObject> bricks;

    [SerializeField]
    int numberOfLayers = 2;

    private float ScreenWidth, ScreenHeight;

    private void Start()
    {
        bricks = new List<GameObject>();
        ScreenWidth = (float)(Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height);
        ScreenHeight = (float)(Camera.main.orthographicSize * 2.0f);

        SpawnBricks();

        ToggleGameObjectsState(false);
    }

    public void SpawnLevel()
    {
        ToggleGameObjectsState(true);

        InitializeBall();
        InitializeBoundaries();
        InitializePlayer();
    }

    public void DisableLevel()
    {
        ball.GetComponent<Rigidbody2D>().Sleep();
        ToggleGameObjectsState(false);
    }
    public int GetBrickCount()
    {
        return bricks.Count;
    }

    private void ToggleGameObjectsState(bool active)
    {
        ball.SetActive(active);
        topWall.SetActive(active);
        leftWall.SetActive(active);
        rightWall.SetActive(active);
        player.SetActive(active);
        foreach (GameObject brick in bricks)
        {
            brick.SetActive(active);
        }
    }

    private void InitializeBall()
    {
        ball.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().WakeUp();
        ball.GetComponent<Rigidbody2D>().AddForce(new(4, 4), ForceMode2D.Impulse);
    }

    /// <summary>
    /// Initialize the Boundaries for ball to hit on top, left and right edges of the screen
    /// </summary>
    private void InitializeBoundaries()
    {
        Vector3 LeftWallPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane));
        Vector3 RightWallPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane));
        Vector3 TopWallPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, Camera.main.nearClipPlane));

        // Spawn the wall boundaries with respect to screen position
        leftWall.transform.SetPositionAndRotation(LeftWallPosition, Quaternion.identity);
        rightWall.transform.SetPositionAndRotation(RightWallPosition, Quaternion.identity);
        topWall.transform.SetPositionAndRotation(TopWallPosition, Quaternion.Euler(0, 0, 90));

        // Make the boundaries change based on screen size
        leftWall.transform.localScale = new(0.1f, ScreenHeight, 1);
        rightWall.transform.localScale = new(0.1f, ScreenHeight, 1);
        topWall.transform.localScale = new(0.1f, ScreenWidth, 1);
    }

    private void InitializePlayer()
    {
        Vector3 playerInitialPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane));
        player.transform.SetPositionAndRotation(playerInitialPosition, Quaternion.Euler(0, 0, 90));
        player.transform.localScale = new Vector3(ScreenWidth / 24.0f, ScreenWidth / 8.0f, 1);
    }

    /// <summary>
    /// Spawn bricks based o number of layers and screen width
    /// </summary>
    private void SpawnBricks()
    {
        var blockWidth = 1 / 8.0f;
        var blockHeight = 1 / 32.0f;
        Vector3 brickScale = new(ScreenWidth/8.0f, ScreenHeight/32.0f, 1);
        var blockPadding = 1 / 80.0f;
        var i = 0;
        var j = numberOfLayers;
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
                       1 - blockXCoord - blockWidth/2.0f -blockPadding,
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
