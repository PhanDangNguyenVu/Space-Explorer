using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;

    private void Start()
    {
        int finalScore = PlayerPrefs.GetInt("SpaceExplorer.FinalScore", 0);

        if (finalScoreText != null)
        {
            finalScoreText.text = "YOUR SCORE: " + finalScore;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
