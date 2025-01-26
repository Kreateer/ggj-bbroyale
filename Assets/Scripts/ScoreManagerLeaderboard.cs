using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class ScoreManagerLeaderboard : MonoBehaviour
{
    public static ScoreManagerLeaderboard instance; //So only one instance is present at any time

    private int currentScore = 0;
    private const string leaderboardKey = "Leaderboard"; //Playerprefs. reference

    [SerializeField] private int maxEntries = 10; //Max. leaderboard entries

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public int score;
    }

    private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();


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
        LoadLeaderboard();
    }

    [System.Serializable]
    private class Wrapper
    {
        public LeaderboardEntry[] entries;
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score updated: {currentScore}");
    }

    public void ResetScore()
    {
        currentScore = 0;
        Debug.Log("Score reset");
    }

    private void SubmitScore(string playerName)
    {
        LeaderboardEntry newEntry = new LeaderboardEntry { playerName = playerName, score = currentScore };
        leaderboard.Add(newEntry);

        leaderboard = leaderboard.OrderByDescending(entry => entry.score).Take(maxEntries).ToList();

        SaveLeaderboard();
        Debug.Log($"Score submitted: {playerName} - {currentScore}");
    }

    private void LoadLeaderboard()
    {
        leaderboard.Clear();

        if (PlayerPrefs.HasKey(leaderboardKey))
        {
            string json = PlayerPrefs.GetString(leaderboardKey);
            LeaderboardEntry[] entries = JsonUtility.FromJson<Wrapper>(json).entries;
            leaderboard = entries.ToList();
        }
        else
        {
            Debug.Log("No leaderboard found. Starting new one");
        }
    }
    private void SaveLeaderboard()
    {
        Wrapper wrapper = new Wrapper { entries = leaderboard.ToArray() };
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(leaderboardKey, json);
        PlayerPrefs.Save();
        Debug.Log("Leaderboard saved.");
    }

    public List<LeaderboardEntry> GetLeaderboard()
    {
        return leaderboard;
    }

}
