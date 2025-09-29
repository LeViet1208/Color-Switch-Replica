using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }

    public GameObject HUD;
    public GameObject menuPanel;

    public TMP_Text bestScore;
    public TMP_Text lastScore;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        Time.timeScale = 0;
    }

    private void Start()
    {
        ShowMenu();
    }

    void ShowMenu()
    {
        menuPanel.SetActive(true);

        HUD.SetActive(false);

        int best = PlayerPrefs.GetInt("BestScore", 0);
        int last = PlayerPrefs.GetInt("LastScore", 0);

        if (bestScore != null)
            bestScore.text = "Best score: " + best;

        if (lastScore != null)
            lastScore.text = "Last play score: " + last;
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        HUD.SetActive(true);

        if (ScoreManager.instance != null)
            ScoreManager.instance.ResetScore();

        if (SoundManager.instance != null)
                SoundManager.instance.PlayClick();
        else
            Debug.Log("Invalid Sound Manager");

        Time.timeScale = 1;
    }

    public void GameOver()
    {
        if (SoundManager.instance != null)
                SoundManager.instance.PlayGameOver();
            else
                Debug.Log("Invalid Sound Manager");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        if (SoundManager.instance != null)
                SoundManager.instance.PlayClick();
        else
            Debug.Log("Invalid Sound Manager");
            
        Application.Quit();
    }
}
