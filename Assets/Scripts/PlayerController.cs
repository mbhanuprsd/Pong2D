using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float paddleSpeed = 5.0f;

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            int direction = (touch.position.x > (Screen.width / 2)) ? 1 : -1;
            MovePaddle(direction);
        }

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
        float playerPosX = Mathf.Clamp(xPos, leftThreshold+transform.localScale.y, rightThreshold - transform.localScale.y);
        transform.position = new Vector3(playerPosX, transform.position.y, transform.position.z);
    }
}
