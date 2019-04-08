
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] GameObject textField;
    private PlayerController player;
    private Text scoreText;
    private const int CONSTANT = 10;

    private void Awake()
    {
        scoreText = textField.GetComponent<Text>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (player.isAlive)
        {
            int score = (int)player.distance;// / CONSTANT;
            scoreText.text = score.ToString();
        }
    }
}
