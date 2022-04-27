using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [SerializeField]
    int ballSpeed = 3;
    private Vector3 bottomCenterPosition;

    // Start is called before the first frame update
    void Start()
    {
        bottomCenterPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, Camera.main.nearClipPlane));
        ResetBall();
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
        Rigidbody2D ballRigidBody = GetComponent<Rigidbody2D>();

        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        if(!ballRigidBody.IsSleeping())
            ballRigidBody.Sleep();
        ballRigidBody.WakeUp();
        Vector2 randomForce = PongUtility.CreateRandomForce(ballSpeed);
        ballRigidBody.AddForce(randomForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            collision.gameObject.SetActive(false);
            PongEvents.current.BrickHit();
        }
    }
}
