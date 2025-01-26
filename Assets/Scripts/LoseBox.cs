using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Lose");
    }
}
