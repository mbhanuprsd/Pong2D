using System.Collections;
using System.Collections.Generic;
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

    void MovePaddle(int direction)
    {
        float xPos = transform.position.x + (direction * Time.deltaTime * paddleSpeed);
        float playerPosX = Mathf.Clamp(xPos, Screen.width*(-0.5f)+transform.localScale.y, Screen.width * 0.5f - transform.localScale.y);
        transform.position = new Vector3(playerPosX, transform.position.y, transform.position.z);
    }
}
