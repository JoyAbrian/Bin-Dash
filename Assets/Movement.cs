using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isDragging)
        {
            // Get the touch position and convert it to world space
            Vector3 touchPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPos.z = 0; // Set z to 0 to keep the object in 2D space
            transform.position = touchPos + offset; // Move the object with the offset
        }

        // Start dragging on touch down
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPos.z = 0;

            // Check if the touch is on the object
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                isDragging = true;
                offset = transform.position - touchPos; // Store the offset
            }
        }

        // Stop dragging on touch end
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isDragging = false;
        }
    }
}
