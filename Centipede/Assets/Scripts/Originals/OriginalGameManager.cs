using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class OriginalGameManager : MonoBehaviour
{
    public static OriginalGameManager Instance { get; private set; }

    private OriginalBlaster blaster;
    private OriginalCentipede centipede;
    private OriginalMushroomField mushroomField;

    public GameObject gameOver;
    public Text scoreText;
    public Text livesText;

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        blaster = FindObjectOfType<OriginalBlaster>();
        centipede = FindObjectOfType<OriginalCentipede>();
        mushroomField = FindObjectOfType<OriginalMushroomField>();

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);

        blaster.Respawn();
        centipede.Respawn();
        mushroomField.Clear();
        mushroomField.Generate();
        gameOver.SetActive(false);
    }

    public void ResetRound()
    {
        SetLives(lives - 1);

        if (lives <= 0)
        {
            GameOver();
            return;
        }

        mushroomField.Heal();
        centipede.Respawn();
        blaster.Respawn();
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        blaster.gameObject.SetActive(false);
    }

    public void IncreaseScore(int amount)
    {
        SetScore(score + amount);
    }

    public void NextLevel()
    {
        centipede.speed *= 1.1f;
        centipede.Respawn();
    }

    private void SetScore(int value)
    {
        score = value;
        scoreText.text = score.ToString();
    }

    private void SetLives(int value)
    {
        lives = Mathf.Max(value, 0);
        livesText.text = lives.ToString();
    }

}
