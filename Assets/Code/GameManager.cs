using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Bu kod oyunun baþlama ve bitme durumlarýný yönetir.
/// </summary>
public class GameManager : MonoBehaviour
{
    private bool isLevelCompleted;
    public static GameManager _instance;

    [Header("Win Lose Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Tekil örneði bu bileþen olarak ayarla
        _instance = this;
    }

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
        SceneManager.LoadScene(0);
    }

    public void StartNextLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}