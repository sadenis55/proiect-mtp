using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image fadeImage;
    public Button playAgain;
    public Button mainmenubutton;

    public TMP_Text textCS;
    public TMP_Text textHS;
    public TMP_Text currentScore;
    public TMP_Text highScore;

    private int score;
    private int highscore;

    private Blade blade;
    private Spawner spawner;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }
    private void Start()
    {
        NewGame();
    }

    public void playAgainButton()
    {
        NewGame();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void NewGame()
    {
        textCS.gameObject.SetActive(false);
        currentScore.gameObject.SetActive(false);
        textHS.gameObject.SetActive(false);
        highScore.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(false);
        mainmenubutton.gameObject.SetActive(false);

        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        ClearScene();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach(Fruit f in fruits)
        {
            Destroy(f.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb b in bombs)
        {
            Destroy(b.gameObject);
        }
    }
    
    public void IncreaseScore()
    {
        score += 10;
        scoreText.text = score.ToString();
    }

    public void UpdateHighScore()
    {
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            highscore = PlayerPrefs.GetInt("SavedHighScore");
            score = int.Parse(scoreText.text);
            score = score - 10;
            if (score > highscore)
            {
                PlayerPrefs.SetInt("SavedHighScore", score);
                highscore = score + 10;
            }
        }
        else
        {
            PlayerPrefs.SetInt("SavedHighScore", score);
            highscore = score + 10;
        }
        currentScore.text = scoreText.text;
        highScore.text = highscore.ToString();
    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());

        
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while(elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        textCS.gameObject.SetActive(true);
        currentScore.gameObject.SetActive(true);
        textHS.gameObject.SetActive(true);
        highScore.gameObject.SetActive(true);

        playAgain.gameObject.SetActive(true);
        mainmenubutton.gameObject.SetActive(true);

        UpdateHighScore();

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(playAgain.GetComponent<RectTransform>(), Input.mousePosition));

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        yield return new WaitForSeconds(1f);
    }
}
