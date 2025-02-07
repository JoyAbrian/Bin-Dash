using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging;
    private Rigidbody2D rb;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 10;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 touchPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPos.z = 0;
            transform.position = touchPos + offset;

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
}