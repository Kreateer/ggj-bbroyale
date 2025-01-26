using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; //So only one instance is present at any time

    private int currentScore = 0;
    private int highScore = 0;

    [SerializeField] private string highScoreKey = "HighScore"; //For prefs. saving


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //So it sticks between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0); //Loads the locally saved score
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score updated: {currentScore}");

        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        Debug.Log("Score reset");
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(highScoreKey, highScore);
        PlayerPrefs.Save();
        Debug.Log($"New High score saved: {highScore}");
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public int GetScore()
    {
        return currentScore;
    }
}
