using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 0f;
        GamePaused = true;
        gameOverScreen.SetActive(true);
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        GamePaused = false;
        gameOverScreen.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Menu()
    {
        Time.timeScale = 1f;
        GamePaused = false;

        SceneManager.LoadScene(0);
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        GamePaused = false;
        pauseMenu.SetActive(false);
    }
}
