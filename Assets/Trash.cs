using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum TrashType { Organik, Anorganik, B3 }

    public TrashType trashType;
    public float fallSpeed = 1f;
    public int score = 1;
}
