using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public enum TrashType { Organik, Anorganik, B3 }
    public TrashType trashType;

    [Header("UI")]
    public Sprite trashBinSprite;
    public Sprite trashBinOpenSprite;
}