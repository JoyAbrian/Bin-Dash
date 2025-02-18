using TMPro;
using UnityEngine;

public class LiveManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro liveText;

    private void Update()
    {
        liveText.text = "Life: " + GlobalVariables.lives.ToString();
    }
}