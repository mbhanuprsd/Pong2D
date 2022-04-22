using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject ballPrefab;
    [SerializeField]
    GameObject wallPrefab;
    
    void Start()
    {
        InitializeBall();

        InitializeBoundaries();
    }

    private void InitializeBall()
    {
        Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
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
        var LeftWall = Instantiate(wallPrefab, LeftWallPosition, Quaternion.identity);
        var RightWall = Instantiate(wallPrefab, RightWallPosition, Quaternion.identity);
        var TopWall = Instantiate(wallPrefab, TopWallPosition, Quaternion.Euler(0, 0, 90));

        // Make the boundaries change based on screen size
        LeftWall.transform.localScale = new Vector3(0.1f, ScreenHeight, 1);
        RightWall.transform.localScale = new Vector3(0.1f, ScreenHeight, 1);
        TopWall.transform.localScale = new Vector3(0.1f, ScreenWidth, 1);
    }    
}
