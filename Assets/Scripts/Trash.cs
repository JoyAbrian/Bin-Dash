using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum TrashType { Organik, Anorganik, B3 }

    public TrashType trashType;
    public int scoreToCollect = 1;
    public int scoreToSpawn = 0;
}