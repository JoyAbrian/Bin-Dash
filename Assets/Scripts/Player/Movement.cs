using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;

        // Calculate screen bounds in world space
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        screenBounds = new Vector2(topRight.x, topRight.y);
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 touchPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPos.z = 0;
            transform.position = ClampPosition(touchPos + offset);

            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPos.z = 0;

            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                isDragging = true;
                offset = transform.position - touchPos;
                rb.velocity = Vector2.zero;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isDragging = false;
            rb.gravityScale = 1;
        }
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        float objectWidth = GetComponent<Collider2D>().bounds.extents.x;
        float objectHeight = GetComponent<Collider2D>().bounds.extents.y;

        float clampedX = Mathf.Clamp(position.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        float clampedY = Mathf.Clamp(position.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        return new Vector3(clampedX, clampedY, 0);
    }
}
