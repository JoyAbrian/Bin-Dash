using UnityEngine;

public class TrashThrown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            int scoreToAdd = other.GetComponent<Trash>().scoreToCollect;
            if (GetComponentInParent<TrashBin>().trashType.ToString() == other.GetComponent<Trash>().trashType.ToString())
            {
                GlobalVariables.score += scoreToAdd;
            }
            else
            {
                GlobalVariables.lives--;
            }
            Destroy(other.gameObject);
        }
    }
}
