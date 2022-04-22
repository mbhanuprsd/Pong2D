using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector3 bottomCenterPosition;

    // Start is called before the first frame update
    void Start()
    {
        bottomCenterPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, Camera.main.nearClipPlane));
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= bottomCenterPosition.y)
        {
            PongEvents.current.BallScreenExit();
            ResetBall();
        }
    }

    private void ResetBall()
    {
        transform.position = Vector3.zero;
    }
}
