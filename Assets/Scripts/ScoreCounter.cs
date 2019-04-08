
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
            int distance = (int)player.distance;
            int coins = player.coins;
            int score = distance + coins * 100;
            scoreText.text = score.ToString();
        }
    }
}
