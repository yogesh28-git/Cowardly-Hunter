using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UI_Controller : MonoBehaviour
{
    public static bool GamePaused = false;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button restart;
    [SerializeField] private Button menu;
    [SerializeField] private Button pRestart;
    [SerializeField] private Button pMenu;
    [SerializeField] private Button pResume;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    private void Start()
    {
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        restart.onClick.AddListener(Restart);
        menu.onClick.AddListener(Menu);
        pRestart.onClick.AddListener(Restart);
        pMenu.onClick.AddListener(Menu);
        pResume.onClick.AddListener(Resume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }  
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        GamePaused = true;
        pauseMenu.SetActive(true);
    }


    public void GameOver()
    {
        AudioManager.Instance.PlayEffects(Sounds.gameOver);
        

        Time.timeScale = 0f;
        GamePaused = true;
        gameOverScreen.SetActive(true);
    }

    private void Restart()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);

        Time.timeScale = 1f;
        GamePaused = false;
        gameOverScreen.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Menu()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);

        Time.timeScale = 1f;
        GamePaused = false;

        SceneManager.LoadScene(0);
    }

    private void Resume()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);

        Time.timeScale = 1f;
        GamePaused = false;
        pauseMenu.SetActive(false);
    }


    public void ScoreIncrease(int increment)
    {
        score += increment;
        scoreText.text = "Score : " + score;
        PlayerPrefs.SetInt("score", score);
    }
}
