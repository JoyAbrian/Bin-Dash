using UnityEngine;

public class Movement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Collider2D col;
    private TrashBin trashBin;

    private bool isDragging = false;
    private Vector3 offset;
    private Vector2 screenBounds;
    private int activeTouchId = -1;

    private static Movement currentDraggingObject = null; 

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        trashBin = GetComponent<TrashBin>();

        rb.gravityScale = 1;

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(Vector3.zero);
        Vector3 topRight = mainCamera.ViewportToWorldPoint(Vector3.one);

        screenBounds = new Vector2(topRight.x, topRight.y);

        IgnoreTrashBinCollisions();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            HandleTouchInput();
        }
        else
        {
            HandleMouseInput();
        }
    }

    private void HandleTouchInput()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPos = mainCamera.ScreenToWorldPoint(touch.position);
        touchPos.z = 0;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (activeTouchId == -1 && col.OverlapPoint(touchPos) && currentDraggingObject == null)
                {
                    activeTouchId = touch.fingerId;
                    StartDragging(touchPos);
                }
                break;

            case TouchPhase.Moved:
                if (isDragging && touch.fingerId == activeTouchId)
                {
                    rb.MovePosition(ClampPosition(touchPos + offset));
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (touch.fingerId == activeTouchId)
                {
                    StopDragging();
                }
                break;
        }
    }

    private void HandleMouseInput()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0) && col.OverlapPoint(mousePos) && currentDraggingObject == null)
        {
            StartDragging(mousePos);
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            rb.MovePosition(ClampPosition(mousePos + offset));
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            StopDragging();
        }
    }

    private void StartDragging(Vector3 position)
    {
        isDragging = true;
        offset = transform.position - position;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        trashBin.OnDrag();

        currentDraggingObject = this;
    }

    private void StopDragging()
    {
        isDragging = false;
        rb.gravityScale = 1;
        trashBin.OnDrop();
        activeTouchId = -1;

        currentDraggingObject = null;
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        float objectWidth = col.bounds.extents.x;
        float objectHeight = col.bounds.extents.y;

        float clampedX = Mathf.Clamp(position.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        float clampedY = Mathf.Clamp(position.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        return new Vector3(clampedX, clampedY, 0);
    }

    private void IgnoreTrashBinCollisions()
    {
        TrashBin[] trashBins = FindObjectsOfType<TrashBin>();

        foreach (TrashBin otherTrashBin in trashBins)
        {
            if (otherTrashBin != trashBin)
            {
                Collider2D otherCollider = otherTrashBin.GetComponent<Collider2D>();
                if (otherCollider != null)
                {
                    Physics2D.IgnoreCollision(col, otherCollider);
                }
            }
        }
    }
}