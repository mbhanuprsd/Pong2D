using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float paddleSpeed = 5.0f;

    void Start()
    {
        ResetPlayer();
    }

    void ResetPlayer()
    {
        gameObject.SetActive(true);

        Vector3 playerInitialPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane));
        transform.SetPositionAndRotation(playerInitialPosition, Quaternion.Euler(0, 0, 90));

        PongUtility.ScreenResolution screenResolution = PongUtility.GetScreenResolution();
        transform.localScale = new(screenResolution.Height / 40.0f, screenResolution.Width / 8.0f, 1);
    }

    void FixedUpdate()
    {
#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            int direction = (touch.position.x > (Screen.width / 2)) ? 1 : -1;
            MovePaddle(direction);
        }
#else
        int direction = (int)Input.GetAxisRaw("Horizontal");
        MovePaddle(direction);
#endif
    }

    /// <summary>
    /// Move the player paddle using touch 
    /// Touch on left side of screen moves the paddle to left and vice versa
    /// </summary>
    /// <param name="direction">Left/Right direction of touch</param>
    void MovePaddle(int direction)
    {
        float leftThreshold = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.1f, Camera.main.nearClipPlane)).x;
        float rightThreshold = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.1f, Camera.main.nearClipPlane)).x;
        float xPos = transform.position.x + (direction * Time.deltaTime * paddleSpeed);
        float playerPosX = Mathf.Clamp(xPos, leftThreshold + transform.localScale.y, rightThreshold - transform.localScale.y);
        transform.position = new Vector3(playerPosX, transform.position.y, transform.position.z);
    }
}
