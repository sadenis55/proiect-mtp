using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image fadeImage;
    public Button playAgain;

    private int score;

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

    private void NewGame()
    {
        playAgain.gameObject.SetActive(false);

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

        playAgain.gameObject.SetActive(true);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(playAgain.GetComponent<RectTransform>(), Input.mousePosition));

        playAgainButton();

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
