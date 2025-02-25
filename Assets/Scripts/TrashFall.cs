using UnityEngine;

public class TrashFall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            other.isTrigger = false;
            other.gameObject.GetComponent<TrashRotate>().enabled = false;
            GlobalVariables.lives--;
        }
    }
}