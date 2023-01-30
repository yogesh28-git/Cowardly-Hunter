using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyScript : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button instructions;
    [SerializeField] private Button highScore;
    [SerializeField] private Button quit;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private Button close;
    [SerializeField] private GameObject details;
    [SerializeField] private GameObject[] pages = new GameObject[4];
    [SerializeField] private TextMeshProUGUI highScoreText;
    private int currentPageIndex;
    void Start()
    {
        play.onClick.AddListener(Play);
        instructions.onClick.AddListener(Instructions);
        highScore.onClick.AddListener(HighScore);
        quit.onClick.AddListener(Quit);
        leftArrow.onClick.AddListener(Left);
        rightArrow.onClick.AddListener(Right);
        close.onClick.AddListener(Close);
    }

    private void Play()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);
        SceneManager.LoadScene(1);
    }
    private void Quit()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);

        Application.Quit();
    }

    private void Instructions()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);

        details.SetActive(true);
        leftArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(true);
        close.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(false);
        pages[0].SetActive(true);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        pages[3].SetActive(false);
        currentPageIndex = 0;
    }

    private void HighScore()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);

        details.SetActive(true);
        pages[0].SetActive(false);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        pages[3].SetActive(false);
        close.gameObject.SetActive(true);

        int score = PlayerPrefs.GetInt("score", 0);
        highScoreText.gameObject.SetActive(true);
        highScoreText.text = "High Score is : " + score;
    }

    private void Left()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);

        rightArrow.gameObject.SetActive(true);
        pages[currentPageIndex].SetActive(false);
        currentPageIndex--;
        pages[currentPageIndex].SetActive(true);
        if(currentPageIndex == 0)
        {
            leftArrow.gameObject.SetActive(false);
        }
    }

    private void Right()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);

        leftArrow.gameObject.SetActive(true);
        pages[currentPageIndex].SetActive(false);
        currentPageIndex++;
        pages[currentPageIndex].SetActive(true);
        if(currentPageIndex == 3)
        {
            rightArrow.gameObject.SetActive(false);
        }
    }
    private void Close()
    {
        AudioManager.Instance.PlayEffects(Sounds.click);

        leftArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(false);
        details.SetActive(false);
    }
}
