using System;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [SerializeField]
    GameObject ball, topWall, leftWall, rightWall, player, brickPrefab;

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

    private void ToggleGameObjectsState(bool active)
    {
        ball.SetActive(active);
        topWall.SetActive(active);
        leftWall.SetActive(active);
        rightWall.SetActive(active);
        player.SetActive(active);
    }

    private void InitializeBall()
    {
        ball.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().WakeUp();
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(4, 4), ForceMode2D.Impulse);
    }

    /// <summary>
    /// Initialize the Boundaries for ball to hit on top, left and right edges of the screen
    /// </summary>
    private void InitializeBoundaries()
    {
        Vector3 LeftWallPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane));
        Vector3 RightWallPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane));
        Vector3 TopWallPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, Camera.main.nearClipPlane));

        float ScreenWidth = (float)(Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height);
        float ScreenHeight = (float)(Camera.main.orthographicSize * 2.0f);

        // Spawn the wall boundaries with respect to screen position
        leftWall.transform.SetPositionAndRotation(LeftWallPosition, Quaternion.identity);
        rightWall.transform.SetPositionAndRotation(RightWallPosition, Quaternion.identity);
        topWall.transform.SetPositionAndRotation(TopWallPosition, Quaternion.Euler(0, 0, 90));

        // Make the boundaries change based on screen size
        leftWall.transform.localScale = new Vector3(0.1f, ScreenHeight, 1);
        rightWall.transform.localScale = new Vector3(0.1f, ScreenHeight, 1);
        topWall.transform.localScale = new Vector3(0.1f, ScreenWidth, 1);
    }

    private void InitializePlayer()
    {
        Vector3 playerInitialPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane));
        player.transform.SetPositionAndRotation(playerInitialPosition, Quaternion.Euler(0, 0, 90));
    }
}
