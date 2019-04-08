
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] GameObject textField;
    private static Text scoreText;

    private void Awake()
    {
        scoreText = textField.GetComponent<Text>();
    }

    private static int scoreValue = 0;
    public static int ScoreValue{
        get { return scoreValue; }
        set {
            scoreValue = value;
            scoreText.text = scoreValue.ToString();
        }
    }
}
