using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Rules")]
    [SerializeField] private int asteroidPenalty = 10;

    private int score;
    private bool gameEnded;

    public int Score => score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        score = 0;
        gameEnded = false;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        if (gameEnded || amount <= 0)
        {
            return;
        }

        score += amount;
        UpdateScoreUI();
    }

    public void PlayerHitAsteroid()
    {
        if (gameEnded)
        {
            return;
        }

        // Tru diem truoc khi ket thuc, khong de diem am.
        score = Mathf.Max(0, score - asteroidPenalty);
        gameEnded = true;

        PlayerPrefs.SetInt("SpaceExplorer.FinalScore", score);
        PlayerPrefs.Save();

        SceneManager.LoadScene("EndGame");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
