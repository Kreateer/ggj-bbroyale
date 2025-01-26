using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardPanel; // The parent panel for the leaderboard
    [SerializeField] private GameObject entryPrefab; // The prefab for each leaderboard entry
    [SerializeField] private Transform entryContainer; // The container to hold the entries
    [SerializeField] private TextMeshProUGUI titleText; // Title of the leaderboard


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLeaderboardUI();
    }

    public void UpdateLeaderboardUI()
    {
        // Clear existing entries
        foreach (Transform child in entryContainer)
        {
            Destroy(child.gameObject);
        }

        // Get leaderboard entries
        List<ScoreManagerLeaderboard.LeaderboardEntry> leaderboard = ScoreManagerLeaderboard.instance.GetLeaderboard();

        // Update title
        titleText.text = "Leaderboard";

        // Populate leaderboard
        foreach (var entry in leaderboard)
        {
            GameObject newEntry = Instantiate(entryPrefab, entryContainer);
            TextMeshProUGUI[] texts = newEntry.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = entry.playerName; // Set player name
            texts[1].text = entry.score.ToString(); // Set score
        }
    }

    public void ToggleLeaderboard()
    {
        leaderboardPanel.SetActive(!leaderboardPanel.activeSelf);
    }
}
