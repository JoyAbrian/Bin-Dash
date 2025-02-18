using UnityEngine;

[RequireComponent(typeof(Trash))]
public class TrashThrown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TrashCan"))
        {
            int scoreToAdd = GetComponent<Trash>().scoreToCollect;
            if (collision.GetComponent<TrashBin>().trashType.ToString() == GetComponent<Trash>().trashType.ToString())
            {
                GlobalVariables.score += scoreToAdd;
                Destroy(gameObject);
            }
            else
            {
                GlobalVariables.lives--;
                Destroy(gameObject);
            }
        }
    }
}
