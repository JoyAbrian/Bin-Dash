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
                GlobalVariables.score += Mathf.FloorToInt(scoreToAdd * 0.2f);
                Destroy(gameObject);
            }
        }
    }
}
