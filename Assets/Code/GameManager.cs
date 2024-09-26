using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Bu kod oyunun baþlama ve bitme durumlarýný yönetir.
/// </summary>
public class GameManager : MonoBehaviour
{
    private bool isLevelCompleted;

    [Header("Win Lose Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    public bool IsLevelCompleted()
    {
        return isLevelCompleted;
    }

    public void SetLevelCompleted()
    {
        winPanel.SetActive(true);
        isLevelCompleted = true;
    }

    public void LevelFailed()
    {
        Time.timeScale = 0;
        losePanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        // Baþka seviye olmadýðý için þimdilik ayný sahneyi tekrar baþlatýyorum
        SceneManager.LoadScene(0);
    }

    public void StartNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}