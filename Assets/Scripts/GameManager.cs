
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text livesText;

    [Header("Lives")]
    [Min(1)]
    public int startingLives = 3;
    public int maxLives = 5;

    private int score = 0;
    private int lives;
    private bool gameEnded = false;

    public int Score => score;
    public int Lives => lives;
    public bool IsGameEnded => gameEnded;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        lives = Mathf.Clamp(startingLives, 1, Mathf.Max(1, maxLives));

        UpdateScoreUI();
        UpdateLivesUI();
    }

    public void AddScore(int amount)
    {
        if (gameEnded)
            return;

        score += amount;
        UpdateScoreUI();
    }

    public void LoseLife()
    {
        if (gameEnded)
            return;

        lives = Mathf.Max(0, lives - 1);
        UpdateLivesUI();

        if (lives <= 0)
        {
            EndGame();
        }
    }

    public void AddLife(int amount = 1)
    {
        if (gameEnded)
            return;

        lives = Mathf.Min(
            Mathf.Max(1, maxLives),
            lives + Mathf.Max(1, amount)
        );

        UpdateLivesUI();
    }

    public void EndGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        PlayerPrefs.SetInt("SpaceExplorer.FinalScore", score);
        PlayerPrefs.Save();

        Time.timeScale = 1f;
        SceneManager.LoadScene("EndGame");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
