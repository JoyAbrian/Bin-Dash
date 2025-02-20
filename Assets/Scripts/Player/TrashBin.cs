using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public enum TrashType { Organik, Anorganik, B3 }
    public TrashType trashType;

    [Header("UI")]
    public GameObject trashBin;
    public GameObject trashBinOpen;

    public void OnDrag()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;

        trashBin.SetActive(false);
        trashBinOpen.SetActive(true);
    }

    public void OnDrop()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;

        trashBin.SetActive(true);
        trashBinOpen.SetActive(false);
    }
}