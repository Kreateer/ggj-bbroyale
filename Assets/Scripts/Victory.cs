using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    private void Start()
    {
        if(scoreText != null)
            scoreText.text = ScoreManager.instance.GetHighScore().ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreManager.instance.AddScore(3333);
            SceneManager.LoadScene("Win");
        }
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Meny");
    }
}
