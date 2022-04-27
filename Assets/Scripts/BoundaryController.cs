using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public enum SIDE
    {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }

    [SerializeField]
    SIDE wallSide;
    
    [SerializeField]
    bool enableCollision = true;

    public SIDE WallSide { get => wallSide; set => wallSide = value; }

    // Start is called before the first frame update
    void Start()
    {
        UpdateWall();
    }

    private void UpdateWall()
    {
        Vector3 wallPosition = Vector3.zero;
        Quaternion wallRotation = Quaternion.identity;
        Vector3 wallScale = Vector3.one;
        PongUtility.ScreenResolution resolution = PongUtility.GetScreenResolution();

        switch (WallSide)
        {
            case SIDE.LEFT:
                wallPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane));
                wallScale = new Vector3(resolution.Width / 20.0f, resolution.Height, 1);
                break;
            case SIDE.RIGHT:
                wallPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane));
                wallScale = new Vector3(resolution.Width / 20.0f, resolution.Height, 1);
                break;
            case SIDE.TOP:
                wallPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, Camera.main.nearClipPlane));
                wallRotation = Quaternion.Euler(0, 0, 90);
                wallScale = new Vector3(resolution.Width / 20.0f, resolution.Width, 1);
                break;
            case SIDE.BOTTOM:
                wallPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, Camera.main.nearClipPlane));
                wallRotation = Quaternion.Euler(0, 0, 90);
                wallScale = new Vector3(resolution.Width / 20.0f, resolution.Width, 1);
                enableCollision = false;
                break;
            default:
                break;
        }

        transform.position = wallPosition;
        transform.rotation = wallRotation;
        transform.localScale = wallScale;

        gameObject.GetComponent<Collider2D>().enabled = enableCollision;

        gameObject.SetActive(true);
    }
}
