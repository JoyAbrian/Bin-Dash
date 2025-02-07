using UnityEngine;

public class TrashThrown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TrashCan"))
        {
            if (collision.GetComponent<TrashBin>().trashType.ToString() == GetComponent<Trash>().trashType.ToString())
            {
                GlobalVariables.score += 10;
                Destroy(gameObject);
            }
            else
            {
                GlobalVariables.score += 5;
                Destroy(gameObject);
            }
        }
    }
}
