
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text highScoreText;

    private const string FinalScoreKey = "SpaceExplorer.FinalScore";
    private const string HighScoreKey = "SpaceExplorer.HighScore";

    private void Start()
    {
        Time.timeScale = 1f;

        // Lay diem cua luot choi hien tai
        int finalScore = PlayerPrefs.GetInt(FinalScoreKey, 0);

        // Lay diem cao nhat da luu
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        // Cap nhat ky luc neu diem hien tai cao hon
        if (finalScore > highScore)
        {
            highScore = finalScore;

            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }

        // Hien thi diem cuoi cung
        if (finalScoreText != null)
        {
            finalScoreText.text = "YOUR SCORE: " + finalScore;
        }

        // Hien thi diem cao nhat
        if (highScoreText != null)
        {
            highScoreText.text = "HIGH SCORE: " + highScore;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

        Debug.Log("Quit Game duoc goi. Trong Unity Editor, game se khong tu dong dong.");
    }
}
