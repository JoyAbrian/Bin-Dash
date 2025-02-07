using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreText;

    private void Update()
    {
        scoreText.text = "Score: " + GlobalVariables.score.ToString();
    }
}
