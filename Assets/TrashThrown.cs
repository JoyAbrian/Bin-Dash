using UnityEngine;

public class TrashThrown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TrashCan"))
        {
            Destroy(gameObject);
        }
    }
}
